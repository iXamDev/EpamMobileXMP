using System;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
using System.Windows.Input;
using FlexiMvvm.Commands;
using System.Threading.Tasks;

namespace XMP.Core.ViewModels.Main
{
    public class MainViewModel : LifecycleViewModel
    {
        protected INavigationService NavigationService { get; }

        public ICommand ShowDetailsCmd => CommandProvider.Get(OnShowDetails);

        public ICommand AddCmd => CommandProvider.Get(OnAdd);

        public MainMenuFilter[] FilterItems;

        public ICommand FilterCmd => CommandProvider.Get<MainMenuFilter>(OnFilter);

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
        }

        public override Task InitializeAsync(bool recreated)
        {
            return base.InitializeAsync(recreated);
        }

        private void OnShowDetails()
        {
            CloseMenuInteraction.RaiseRequested();

            NavigationService.NavigateToDetails(this);
        }

        private void OnAdd()
        {
            CloseMenuInteraction.RaiseRequested();

            NavigationService.NavigateToDetails(this);
        }

        private void OnFilter(MainMenuFilter mainMenuFilter)
        {

        }
    }
}
