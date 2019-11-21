using System;
using FlexiMvvm.Navigation;
using XMP.Core.Navigation;
using XMP.Core.ViewModels.Launcher;
using XMP.Droid.Views.Splash;
using XMP.Droid.Views.Login;
using XMP.Core.ViewModels.Main;
using XMP.Core.ViewModels.Login;
using XMP.Droid.Views.Main;
using XMP.Droid.Views.Details;
using XMP.Core.ViewModels.Details;

namespace XMP.Droid.Navigation
{
    public class AppNavigationService : NavigationService, INavigationService
    {
        public AppNavigationService()
        {
        }

        public new void NavigateBack(DetailsViewModel fromViewModel)
        {
            var fromView = NavigationViewProvider.GetActivity<DetailsActivity, DetailsViewModel>(fromViewModel);

            fromView.Finish();
        }

        public void NavigateToDetails(MainViewModel fromViewModel)
        {
            var fromView = NavigationViewProvider.GetActivity<MainActivity, MainViewModel>(fromViewModel);

            Navigate<DetailsActivity>(fromView);
        }

        public void NavigateToHome(LauncherViewModel fromViewModel)
        {
            throw new NotImplementedException();
        }

        public void NavigateToLogin(LauncherViewModel fromViewModel)
        {
            var fromView = NavigationViewProvider.GetActivity<SplashActivity, LauncherViewModel>(fromViewModel);

            Navigate<LoginActivity>(fromView);
        }

        public void NavigateToMain(LoginViewModel fromViewModel)
        {
            var fromView = NavigationViewProvider.GetActivity<LoginActivity, LoginViewModel>(fromViewModel);

            Navigate<MainActivity>(fromView);
        }
    }
}
