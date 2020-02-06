using System.Threading.Tasks;
using FlexiMvvm.ViewModels;
using NN.Shared.FlexiMvvm.Navigation;
using XMP.Core.Services.Abstract;
using XMP.Core.ViewModels.Login;
using XMP.Core.ViewModels.Main;

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

            _ = Task.Run(NavigateToMainScreen);
        }

        private async Task NavigateToMainScreen()
        {
            await Task.Delay(3000);

            var navigateToMain = await SessionService.Reactivate();

            if (navigateToMain)
                await NavigationService.Navigate<MainViewModel>();
            else
                await NavigationService.Navigate<LoginViewModel>();
        }
    }
}
