using System;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
using System.Windows.Input;
using FlexiMvvm.Commands;
using System.Threading.Tasks;
using System.Collections.Generic;
using XMP.Core.ViewModels.Main.Items;
using FlexiMvvm.Collections;
using XMP.Core.Models;
using System.Linq;

namespace XMP.Core.ViewModels.Main
{
    public class MainViewModel : LifecycleViewModel
    {
        protected INavigationService NavigationService { get; }

        public ICommand ShowDetailsCmd => CommandProvider.Get<VacationRequestItemVM>(OnShowDetails);

        public ICommand AddCmd => CommandProvider.Get(OnAdd);

        public IEnumerable<FilterItemVM> FilterItems { get; }

        public ObservableCollection<VacationRequestItemVM> RequestItems { get; }

        public ICommand FilterCmd => CommandProvider.Get<FilterItemVM>(OnFilter);

        public Interaction CloseMenuInteraction { get; } = new Interaction();

        private string userName = "Arkadiy Dobkin";
        public string UserName
        {
            get => userName;
            private set => SetValue(ref userName, value, nameof(UserName));
        }

        public string AddButtonTitle => "New";

        public string ScreenTitle => "All Requests";

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            //TODO clean up

            FilterItems = new FilterItemVM[]
            {
                new FilterItemVM("All".ToUpper()),
                new FilterItemVM("OPEN".ToUpper()),
                new FilterItemVM("CLOSED".ToUpper())
            };

            var mockRequests = new[]
            {
                new VacantionRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    StartDate = new DateTime(2019,2,3),
                    EndDate = new DateTime(2019,2,12),
                    VacationType = VacationType.Regular,
                    State = VacationState.Approved
                },
                new VacantionRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    StartDate = new DateTime(2019,2,15),
                    EndDate = new DateTime(2019,2,15),
                    VacationType = VacationType.ExceptionalLeave,
                    State = VacationState.Approved
                },
                new VacantionRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    StartDate = new DateTime(2019,3,20),
                    EndDate = new DateTime(2019,4,14),
                    VacationType = VacationType.Overtime,
                    State = VacationState.Approved
                },
                new VacantionRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    StartDate = new DateTime(2019,3,20),
                    EndDate = new DateTime(2019,4,14),
                    VacationType = VacationType.SickDays,
                    State = VacationState.Closed
                },
                new VacantionRequest
                {
                    Id = Guid.NewGuid().ToString(),
                    StartDate = new DateTime(2019,7,20),
                    EndDate = new DateTime(2019,7,26),
                    VacationType = VacationType.NotPayable,
                    State = VacationState.Approved
                },
            };

            RequestItems = new ObservableCollection<VacationRequestItemVM>(mockRequests.Select(SetupRequestItemVM));
        }

        public override Task InitializeAsync(bool recreated)
        {
            return base.InitializeAsync(recreated);
        }

        private void OnAdd()
        {
            NavigationService.NavigateToDetails(this);
        }

        private void OnFilter(FilterItemVM filterVM)
        {
            Console.WriteLine(filterVM.Title);

            CloseMenuInteraction.RaiseRequested();
        }

        private void OnShowDetails(VacationRequestItemVM itemVM)
        {
            CloseMenuInteraction.RaiseRequested();

            NavigationService.NavigateToDetails(this);
        }

        private VacationRequestItemVM SetupRequestItemVM(VacantionRequest model)
        {
            var itemVM = new VacationRequestItemVM();

            itemVM.SetModel(model);

            return itemVM;
        }
    }
}
