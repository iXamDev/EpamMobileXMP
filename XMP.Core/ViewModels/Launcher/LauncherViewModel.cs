using System;
using System.Threading.Tasks;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
using Xamarin.Essentials;
using XMP.Core.Services.Abstract;
namespace XMP.Core.ViewModels.Launcher
{
    public class LauncherViewModel : LifecycleViewModel
    {

        public LauncherViewModel(INavigationService navigationService, ISessionService sessionService)
        {
            NavigationService = navigationService;

            SessionService = sessionService;
        }

        protected INavigationService NavigationService { get; }

        protected ISessionService SessionService { get; }

        public override async Task InitializeAsync(bool recreated)
        {
            await base.InitializeAsync(recreated);

            Task.Run(NavigateToMainScreen);
        }

        private async Task NavigateToMainScreen()
        {
            await Task.Delay(3000);

            var navigateToMain = await SessionService.Reactivate();

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (navigateToMain)
                    NavigationService.NavigateToMain(this);
                else
                    NavigationService.NavigateToLogin(this);
            });
        }
    }
}
