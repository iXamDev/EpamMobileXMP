using System;
using System.Threading.Tasks;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
using Xamarin.Essentials;
namespace XMP.Core.ViewModels.Launcher
{
    public class LauncherViewModel : LifecycleViewModel
    {
        protected INavigationService NavigationService { get; }

        public LauncherViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public override async Task InitializeAsync(bool recreated)
        {
            await base.InitializeAsync(recreated);

            Task.Run(NavigateToMainScreen);
        }

        private async Task NavigateToMainScreen()
        {
            await Task.Delay(3000);

            await MainThread.InvokeOnMainThreadAsync(() => NavigationService.NavigateToLogin(this));
        }
    }
}
