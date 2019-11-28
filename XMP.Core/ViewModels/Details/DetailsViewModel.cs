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

namespace XMP.Core.ViewModels.Details
{
    public class DetailsViewModel : LifecycleViewModel
    {
        public DetailsViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            SetupVacationTypeItems();

            //TODO
            StartDate = new DateTime(2019, 1, 2);

            EndDate = new DateTime(2020, 11, 27);

            VacationState = VacationState.Closed;

            SelectedVacationType = VacationTypeItems.ElementAt(3);
        }

        protected INavigationService NavigationService { get; }

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

        public VacationState[] States { get; } = GetEnumStates<VacationState>();

        private VacationState vacationState;
        public VacationState VacationState
        {
            get => vacationState;
            set => SetValue(ref vacationState, value, nameof(VacationState));
        }

        private void OnSave()
        => NavigationService.NavigateBack(this);

        private async Task<DateTime> ChangeDate(DateTime selectedDate)
        {
            var dialogResult = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig { SelectedDate = StartDate });

            if (dialogResult.Ok)
                return dialogResult.SelectedDate;

            return selectedDate;
        }

        private async Task OnShowStartDateDialog()
        => StartDate = await ChangeDate(StartDate);

        private async Task OnShowEndDateDialog()
        => EndDate = await ChangeDate(EndDate);

        private static T[] GetEnumStates<T>()
        {
            var varians = Enum.GetValues(typeof(T));

            var newItems = new List<T>();

            foreach (var item in varians)
                newItems.Add((T)item);

            return newItems.ToArray();
        }

        private void SetupVacationTypeItems()
        {
            var varians = GetEnumStates<VacationType>();

            VacationTypeItems = varians.Select(SetupItem).ToList();
        }

        private DetailsItemVM SetupItem(VacationType vacationType)
        => new DetailsItemVM(vacationType);
    }
}
