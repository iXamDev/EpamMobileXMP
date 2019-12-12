using XMP.Core.ViewModels.Details;
using XMP.Core.ViewModels.Launcher;
using XMP.Core.ViewModels.Login;
using XMP.Core.ViewModels.Main;

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
