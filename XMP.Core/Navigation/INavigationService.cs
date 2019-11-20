using System;
using XMP.Core.ViewModels.Launcher;

namespace XMP.Core.Navigation
{
    public interface INavigationService
    {
        void NavigateToHome(LauncherViewModel fromViewModel);

        void NavigateToLogin(LauncherViewModel fromViewModel);
    }
}
