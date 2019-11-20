using System;
using FlexiMvvm.Navigation;
using XMP.Core.Navigation;
using XMP.Core.ViewModels.Launcher;

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
            throw new NotImplementedException();
        }
    }
}
