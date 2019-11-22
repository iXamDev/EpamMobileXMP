using System;
using XMP.Core.ViewModels.Launcher;
using XMP.Core.ViewModels.Login;
using XMP.Core.ViewModels.Main;
using XMP.Core.ViewModels.Details;

namespace XMP.Core.Navigation
{
    public interface INavigationService
    {
        void NavigateToLogin(LauncherViewModel fromViewModel);

        void NavigateToDetails(MainViewModel fromViewModel);

        void NavigateToMain(LoginViewModel fromViewModel);

        void NavigateBack(DetailsViewModel fromViewModel);
    }
}
