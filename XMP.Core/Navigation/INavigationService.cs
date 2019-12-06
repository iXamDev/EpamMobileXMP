using System;
using XMP.Core.ViewModels.Launcher;
using XMP.Core.ViewModels.Login;
using XMP.Core.ViewModels.Main;
using XMP.Core.ViewModels.Details;

namespace XMP.Core.Navigation
{
    public interface INavigationService
    {
        void NavigateToLogin();

        void NavigateToLogin(LauncherViewModel fromViewModel);

        void NavigateToDetails(MainViewModel fromViewModel, DetailsParameters detailsParameters);

        void NavigateToMain(LoginViewModel fromViewModel);

        void NavigateToMain(LauncherViewModel fromViewModel);

        void NavigateBack(DetailsViewModel fromViewModel);
    }
}
