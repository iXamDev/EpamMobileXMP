using System;
using FlexiMvvm.Navigation;
using XMP.Core.Navigation;
using XMP.Core.ViewModels.Details;
using XMP.Core.ViewModels.Launcher;
using XMP.Core.ViewModels.Login;
using XMP.Core.ViewModels.Main;
using XMP.iOS.Views.Splash;
using XMP.iOS.Views.Login;
using UIKit;
using XMP.iOS.Views.Main;
using XMP.iOS.Views.Details;

namespace XMP.iOS.Navigation
{
    public class AppNavigationService : NavigationService, INavigationService
    {
        protected UIWindow RootWindow => AppDelegate.Window;

        protected void ReplaceRootViewController(UIViewController newRoot, bool wrapInNavigationController = false)
        {
            var oldRootVC = RootWindow.RootViewController;

            if (wrapInNavigationController)
                newRoot = new UINavigationController(newRoot);

            RootWindow.RootViewController = newRoot;

            oldRootVC.RemoveFromParentViewController();

            oldRootVC.DidMoveToParentViewController(null);
        }

        public void NavigateBack(DetailsViewModel fromViewModel)
        {
            var fromViewController = NavigationViewProvider.GetViewController<DetailsViewController, DetailsViewModel>(fromViewModel);

            NavigateBack(fromViewController, true);
        }

        public void NavigateToDetails(MainViewModel fromViewModel)
        {
            var fromViewController = NavigationViewProvider.GetViewController<MainViewController, MainViewModel>(fromViewModel);

            var newViewController = new DetailsViewController();

            Navigate(fromViewController, newViewController, true);
        }

        public void NavigateToLogin(LauncherViewModel fromViewModel)
        => ReplaceRootViewController(new LoginViewController());

        public void NavigateToMain(LoginViewModel fromViewModel)
        => ReplaceRootViewController(new MainViewController(), true);
    }
}
