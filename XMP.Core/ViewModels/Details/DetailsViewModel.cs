using System;
using System.Threading.Tasks;
using System.Windows.Input;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
using Acr.UserDialogs;
using System.Collections.Generic;
using XMP.Core.ViewModels.Details.Items;
using XMP.Core.Models;
using System.Linq;
using XMP.Core.Helpers;
using XMP.Core.Services.Abstract;
using XMP.Core.Mapping;

namespace XMP.Core.ViewModels.Details
{
    public class DetailsViewModel : LifecycleViewModel<DetailsParameters>
    {
        public DetailsViewModel(ISessionService sessionService, IVacationRequestsManagerService vacationRequestsManagerService, INavigationService navigationService, IUserDialogs userDialogs)
        {
            NavigationService = navigationService;

            UserDialogs = userDialogs;

            SetupVacationTypeItems();

            VacationRequestsManagerService = vacationRequestsManagerService;

            SessionService = sessionService;
        }

        private bool createNew;

        private VacantionRequest model;

        protected IUserDialogs UserDialogs { get; }

        protected INavigationService NavigationService { get; }

        protected ISessionService SessionService { get; }

        protected IVacationRequestsManagerService VacationRequestsManagerService { get; }

        public ICommand SaveCmd => CommandProvider.Get(OnSave);

        public ICommand ShowStartDateDialogCmd => CommandProvider.GetForAsync(OnShowStartDateDialog);

        public ICommand ShowEndDateDialogCmd => CommandProvider.GetForAsync(OnShowEndDateDialog);

        public string ScreenTitle => "Request";

        public List<DetailsItemVM> VacationTypeItems { get; private set; }

        private DetailsItemVM selectedVacationType;
        public DetailsItemVM SelectedVacationType
        {
            get => selectedVacationType;
            set => SetValue(ref selectedVacationType, value, nameof(SelectedVacationType));
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            private set => SetValue(ref startDate, value, nameof(StartDate));
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get => endDate;
            private set => SetValue(ref endDate, value, nameof(EndDate));
        }

        public VacationState[] States { get; } = EnumHelper.GetEnumStates<VacationState>();

        private VacationState vacationState;
        public VacationState VacationState
        {
            get => vacationState;
            set => SetValue(ref vacationState, value, nameof(VacationState));
        }

        private void OnSave()
        {
            if (Validate())
            {
                if (createNew)
                    CreateNewRequest();
                else
                    UpdateRequestIfNeeded();

                NavigationService.NavigateBack(this);
            }
        }

        private void UpdateRequestIfNeeded()
        {
            if (model != null)
            {
                model.Start = StartDate.Date.VacationDateToDateTimeOffset();
                model.End = EndDate.Date.VacationDateToDateTimeOffset();
                model.VacationType = SelectedVacationType.Type;
                model.State = VacationState;

                VacationRequestsManagerService.UpdateVacation(model);
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
            model = request;

            createNew = request == null;

            StartDate = request?.Start.DateTime ?? DateTime.Now.Date;

            EndDate = request?.End.DateTime ?? DateTime.Now.Date;

            SelectedVacationType = request == null ? VacationTypeItems.FirstOrDefault() : VacationTypeItems.FirstOrDefault(t => t.Type == request.VacationType);

            VacationState = request == null ? VacationState.Approved : request.State;
        }
    }
}
