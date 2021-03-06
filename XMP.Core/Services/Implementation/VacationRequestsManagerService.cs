﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExpressMapper.Extensions;
using XMP.API.Models;
using XMP.API.Services.Abstract;
using XMP.Core.Database.Abstract;
using XMP.Core.Models;
using XMP.Core.Services.Abstract;

namespace XMP.Core.Services.Implementation
{
    public class VacationRequestsManagerService : IVacationRequestsManagerService
    {
        private object _syncRoot = new object();

        private CancellationTokenSource _syncCTS;

        private TaskCompletionSource<bool> _syncTCS;

        public VacationRequestsManagerService(IVacationRequestsRepository vacationRequestsRepository, IVacationRequestsApiService vacationRequestsApiService)
        {
            VacationRequestsApiService = vacationRequestsApiService;

            VacationRequestsRepository = vacationRequestsRepository;
        }

        public event EventHandler<EventArgs<VacantionRequest>> VacationAdded;

        public event EventHandler<EventArgs<VacantionRequest>> VacationChanged;

        public event EventHandler VacationsChanged;

        protected IVacationRequestsRepository VacationRequestsRepository { get; }

        protected IVacationRequestsApiService VacationRequestsApiService { get; }

        public IEnumerable<VacantionRequest> GetVacantionRequests()
        => VacationRequestsRepository.GetAll();

        public Task<bool> Sync(bool cancelCurrentSync = false)
        {
            lock (_syncRoot)
            {
                if (_syncTCS != null)
                {
                    if (cancelCurrentSync)
                        CancelCurrentSync();
                    else
                        return _syncTCS.Task;
                }

                ExecuteNewSync();

                return _syncTCS.Task;
            }
        }

        public void UpdateVacation(VacantionRequest vacation)
        {
            var current = VacationRequestsRepository.GetByKey(vacation.LocalId);

            if (current.Equals(vacation))
                return;

            if (vacation.SyncState == SynchronizationState.Synced)
                vacation.SyncState = SynchronizationState.Changed;

            ChangeLocalDataAndRunSync(() => VacationRequestsRepository.Update(vacation));

            RaiseVacationChanged(vacation);
        }

        public void AddVacation(NewVacationRequest newVacantion)
        {
            var vacation = SetupVacationRequest(newVacantion);

            ChangeLocalDataAndRunSync(() => VacationRequestsRepository.Add(vacation));

            RaiseVacationAdded(vacation);
        }

        public VacantionRequest GetVacantion(string localId)
        => VacationRequestsRepository.GetByKey(localId);

        protected string GenerateLocalId()
        => Guid.NewGuid().ToString();

        private VacantionRequest SetupVacationRequest(VacationDto dto)
        => dto.Map<VacationDto, VacantionRequest>();

        private void RaiseVacationAdded(VacantionRequest vacation)
        => VacationAdded?.Invoke(this, new EventArgs<VacantionRequest>(vacation));

        private void RaiseVacationChanged(VacantionRequest vacation)
        => VacationChanged?.Invoke(this, new EventArgs<VacantionRequest>(vacation));

        private void RaiseVacationsChanged()
        => VacationsChanged?.Invoke(this, EventArgs.Empty);

        private VacantionRequest SetupVacationRequest(NewVacationRequest newVacantion)
        {
            var vacation = newVacantion.Map<NewVacationRequest, VacantionRequest>();

            vacation.LocalId = GenerateLocalId();

            vacation.Id = Guid.Empty.ToString();

            vacation.SyncState = SynchronizationState.New;

            vacation.Created = DateTimeOffset.UtcNow;

            return vacation;
        }

        private void CancelCurrentSync()
        {
            _syncCTS?.Cancel();

            _syncCTS?.Dispose();

            _syncCTS = null;
        }

        private void ChangeLocalDataAndRunSync(Action changeAction)
        {
            lock (_syncRoot)
            {
                CancelCurrentSync();

                changeAction();

                ExecuteNewSync();
            }
        }

        private void ExecuteNewSync()
        {
            _syncCTS = new CancellationTokenSource();

            if (_syncTCS == null)
                _syncTCS = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);

            var cancellationToken = _syncCTS.Token;

            Task.Run(() => Synchronization(cancellationToken));
        }

        private async Task Synchronization(CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                var changedModels =
                    VacationRequestsRepository.GetAll()
                    .Where((arg) => arg.SyncState != SynchronizationState.Synced)
                    .ToList();

                var uploadSuccess = !changedModels.Any();

                if (!uploadSuccess)
                {
                    var uploadNotSyncModels = await UploadSynchronization(changedModels, cancellationToken);

                    uploadSuccess = uploadNotSyncModels;
                }

                if (uploadSuccess)
                {
                    var vacationDtos = await VacationRequestsApiService.GetVacations(cancellationToken);

                    var backendModels = vacationDtos
                        ?.Select(SetupVacationRequest)
                        .ToList()
                        ?? new List<VacantionRequest>();

                    cancellationToken.ThrowIfCancellationRequested();

                    var localSyncedModels = VacationRequestsRepository
                        .GetAll()
                        .Where(t => t.SyncState == SynchronizationState.Synced)
                        .ToList();

                    var removedModels = localSyncedModels
                        .Any(l => backendModels
                        .All(b => b.Id != l.Id));

                    if (removedModels || (backendModels.Any() && !localSyncedModels.Any()))
                        ResetLocalItems(localSyncedModels, backendModels, cancellationToken);
                    else
                        UpdateLocalItems(localSyncedModels, backendModels, cancellationToken);

                    success = true;
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception)
            {
            }

            lock (_syncRoot)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                _syncTCS.TrySetResult(success);

                _syncTCS = null;
            }
        }

        private void UpdateLocalItems(IEnumerable<VacantionRequest> localSyncedModels, IEnumerable<VacantionRequest> backendModels, CancellationToken cancellationToken)
        {
            foreach (var backendModel in backendModels)
            {
                var local = localSyncedModels.FirstOrDefault(t => t.Id == backendModel.Id);

                if (local != null)
                {
                    backendModel.LocalId = local.Id;

                    if (!backendModel.Equals(local))
                    {
                        lock (_syncRoot)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            UpdateByBackendModel(backendModel);
                        }

                        RaiseVacationChanged(backendModel);
                    }
                }
                else
                {
                    lock (_syncRoot)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        AddNewBackendModel(backendModel);
                    }

                    RaiseVacationAdded(backendModel);
                }
            }
        }

        private void UpdateByBackendModel(VacantionRequest backendModel)
        {
            backendModel.SyncState = SynchronizationState.Synced;

            VacationRequestsRepository.Update(backendModel);
        }

        private void AddNewBackendModel(VacantionRequest backendModel)
        {
            backendModel.LocalId = backendModel.Id;

            backendModel.SyncState = SynchronizationState.Synced;

            VacationRequestsRepository.Add(backendModel);
        }

        private void ResetLocalItems(IEnumerable<VacantionRequest> localSyncedModels, IEnumerable<VacantionRequest> backendModels, CancellationToken cancellationToken)
        {
            lock (_syncRoot)
            {
                VacationRequestsRepository.RemoveRange(localSyncedModels);

                cancellationToken.ThrowIfCancellationRequested();

                foreach (var item in backendModels)
                    item.LocalId = item.Id;

                VacationRequestsRepository.AddRange(backendModels);
            }

            RaiseVacationsChanged();
        }

        private async Task<bool> UploadSynchronization(IEnumerable<VacantionRequest> changedModels, CancellationToken cancellationToken)
        {
            foreach (var model in changedModels)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var dto = SetupVacationDto(model);

                if (model.SyncState == SynchronizationState.New)
                    await CreateOnServer(model, dto, cancellationToken);
                else
                    await UpdateOnServer(model, dto, cancellationToken);

                model.SyncState = SynchronizationState.Synced;

                lock (_syncRoot)
                    VacationRequestsRepository.Update(model);
            }

            return true;
        }

        private Task UpdateOnServer(VacantionRequest model, VacationDto dto, CancellationToken cancellationToken)
        => VacationRequestsApiService.UpdateVacation(dto, cancellationToken);

        private async Task CreateOnServer(VacantionRequest model, VacationDto dto, CancellationToken cancellationToken)
        {
            var newDto = await VacationRequestsApiService.CreateVacation(dto, cancellationToken);

            model.Id = newDto.Id;
        }

        private VacationDto SetupVacationDto(VacantionRequest model)
        {
            var dto = model.Map<VacantionRequest, VacationDto>();

            return dto;
        }
    }
}
