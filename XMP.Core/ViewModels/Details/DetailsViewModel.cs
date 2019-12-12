using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FlexiMvvm.ViewModels;
using XMP.Core.Helpers;
using XMP.Core.Mapping;
using XMP.Core.Models;
using XMP.Core.Navigation;
using XMP.Core.Services.Abstract;
using XMP.Core.ViewModels.Details.Items;

namespace XMP.Core.ViewModels.Details
{
    public class DetailsViewModel : LifecycleViewModel<DetailsParameters>
    {
        private DetailsItemVM _selectedVacationType;

        private bool _createNew;

        private VacantionRequest _model;

        private DateTime _startDate;

        private DateTime _endDate;

        private VacationState _vacationState;

        public DetailsViewModel(ISessionService sessionService, IVacationRequestsManagerService vacationRequestsManagerService, INavigationService navigationService, IUserDialogs userDialogs)
        {
            NavigationService = navigationService;

            UserDialogs = userDialogs;

            SetupVacationTypeItems();

            VacationRequestsManagerService = vacationRequestsManagerService;

            SessionService = sessionService;
        }

        protected IUserDialogs UserDialogs { get; }

        protected INavigationService NavigationService { get; }

        protected ISessionService SessionService { get; }

        protected IVacationRequestsManagerService VacationRequestsManagerService { get; }

        public ICommand SaveCmd => CommandProvider.Get(OnSave);

        public ICommand ShowStartDateDialogCmd => CommandProvider.GetForAsync(OnShowStartDateDialog);

        public ICommand ShowEndDateDialogCmd => CommandProvider.GetForAsync(OnShowEndDateDialog);

        public string ScreenTitle => "Request";

        public List<DetailsItemVM> VacationTypeItems { get; private set; }

        public DetailsItemVM SelectedVacationType
        {
            get => _selectedVacationType;
            set => SetValue(ref _selectedVacationType, value, nameof(SelectedVacationType));
        }

        public DateTime StartDate
        {
            get => _startDate;
            private set => SetValue(ref _startDate, value, nameof(StartDate));
        }

        public DateTime EndDate
        {
            get => _endDate;
            private set => SetValue(ref _endDate, value, nameof(EndDate));
        }

        public VacationState[] AvailableVacationStates { get; } = { VacationState.Approved, VacationState.Closed };

        public VacationState VacationState
        {
            get => _vacationState;
            set => SetValue(ref _vacationState, value, nameof(VacationState));
        }

        private void OnSave()
        {
            if (Validate())
            {
                if (_createNew)
                    CreateNewRequest();
                else
                    UpdateRequestIfNeeded();

                NavigationService.NavigateBack(this);
            }
        }

        private void UpdateRequestIfNeeded()
        {
            if (_model != null)
            {
                _model.Start = StartDate.Date.VacationDateToDateTimeOffset();
                _model.End = EndDate.Date.VacationDateToDateTimeOffset();
                _model.VacationType = SelectedVacationType.Type;
                _model.State = VacationState;

                VacationRequestsManagerService.UpdateVacation(_model);
            }
        }

        private void CreateNewRequest()
        {
            VacationRequestsManagerService.AddVacation(new NewVacationRequest
            {
                CreatedBy = SessionService.UserLogin,
                Start = StartDate,
                End = EndDate,
                VacationType = SelectedVacationType.Type,
                State = VacationState
            });
        }

        private bool Validate()
        {
            if (StartDate.Date > EndDate.Date)
            {
                UserDialogs.Alert("Start date should be less than end date");

                return false;
            }

            return true;
        }

        private async Task<DateTime> ChangeDate(DateTime selectedDate)
        {
            var dialogResult = await UserDialogs.DatePromptAsync(new DatePromptConfig { SelectedDate = selectedDate });

            if (dialogResult.Ok)
                return dialogResult.SelectedDate;

            return selectedDate;
        }

        private async Task OnShowStartDateDialog()
        => StartDate = await ChangeDate(StartDate);

        private async Task OnShowEndDateDialog()
        => EndDate = await ChangeDate(EndDate);

        private void SetupVacationTypeItems()
        {
            var varians = EnumHelper.GetEnumStates<VacationType>();

            VacationTypeItems = varians.Select(SetupItem).ToList();
        }

        private DetailsItemVM SetupItem(VacationType vacationType)
        => new DetailsItemVM(vacationType);

        public override void Initialize(DetailsParameters parameters, bool recreated)
        {
            SetModel((parameters?.CreateNew ?? true) ? null : VacationRequestsManagerService.GetVacantion(parameters.LocalId));

            base.Initialize(parameters, recreated);
        }

        private void SetModel(VacantionRequest request)
        {
            _model = request;

            _createNew = request == null;

            StartDate = request?.Start.DateTime ?? DateTime.Now.Date;

            EndDate = request?.End.DateTime ?? DateTime.Now.Date;

            SelectedVacationType = request == null ? VacationTypeItems.FirstOrDefault() : VacationTypeItems.FirstOrDefault(t => t.Type == request.VacationType);

            VacationState = request == null ? VacationState.Approved : request.State;
        }
    }
}
