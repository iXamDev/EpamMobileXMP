using FlexiMvvm.Navigation;
using UIKit;
using XMP.Core.Navigation;
using XMP.Core.ViewModels.Details;
using XMP.Core.ViewModels.Launcher;
using XMP.Core.ViewModels.Login;
using XMP.Core.ViewModels.Main;
using XMP.iOS.Views.Details;
using XMP.iOS.Views.Login;
using XMP.iOS.Views.Main;

namespace XMP.iOS.Navigation
{
    public class AppNavigationService : NavigationService, INavigationService
    {
        protected UIWindow RootWindow => AppDelegate.Window;

        public void NavigateBack(DetailsViewModel fromViewModel)
        {
            var fromViewController = NavigationViewProvider.GetViewController<DetailsViewController, DetailsViewModel>(fromViewModel);

            NavigateBack(fromViewController, true);
        }

        public void NavigateToLogin(LauncherViewModel fromViewModel)
        => ReplaceRootViewController(new LoginViewController());

        public void NavigateToMain(LoginViewModel fromViewModel)
        => ReplaceRootViewController(new MainViewController(), true);

        public void NavigateToMain(LauncherViewModel fromViewModel)
        => ReplaceRootViewController(new MainViewController(), true);

        public void NavigateToDetails(MainViewModel fromViewModel, DetailsParameters detailsParameters)
        {
            var fromViewController = NavigationViewProvider.GetViewController<MainViewController, MainViewModel>(fromViewModel);

            var newViewController = new DetailsViewController();

            Navigate(fromViewController, newViewController, detailsParameters, true);
        }

        public void NavigateToLogin()
        => ReplaceRootViewController(new LoginViewController());

        protected void ReplaceRootViewController(UIViewController newRoot, bool wrapInNavigationController = false)
        {
            var oldRootVC = RootWindow.RootViewController;

            if (wrapInNavigationController)
                newRoot = new UINavigationController(newRoot);

            RootWindow.RootViewController = newRoot;

            oldRootVC.RemoveFromParentViewController();

            oldRootVC.DidMoveToParentViewController(null);
        }
    }
}
