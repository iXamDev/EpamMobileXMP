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

        public ICommand TestCmd => CommandProvider.Get(ShowDetails);

        public MainMenuFilter[] FilterItems;

        public ICommand FilterCmd => CommandProvider.Get<MainMenuFilter>(OnFilter);

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public override Task InitializeAsync(bool recreated)
        {
            return base.InitializeAsync(recreated);
        }

        private void ShowDetails()
        {
            NavigationService.NavigateToDetails(this);
        }

        private void OnFilter(MainMenuFilter mainMenuFilter)
        {

        }
    }
}
