using System;
using FlexiMvvm.Navigation;
using XMP.Core.Navigation;
using XMP.Core.ViewModels.Launcher;
using XMP.Droid.Views.Splash;
using XMP.Droid.Views.Login;

namespace XMP.Droid.Navigation
{
    public class AppNavigationService : NavigationService, INavigationService
    {
        public AppNavigationService()
        {
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
    }
}
