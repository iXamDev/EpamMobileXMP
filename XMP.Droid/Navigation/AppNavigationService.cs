using Android.Content;
using FlexiMvvm.Navigation;
using XMP.Core.Navigation;
using XMP.Core.ViewModels.Details;
using XMP.Core.ViewModels.Launcher;
using XMP.Core.ViewModels.Login;
using XMP.Core.ViewModels.Main;
using XMP.Droid.Views.Details;
using XMP.Droid.Views.Login;
using XMP.Droid.Views.Main;
using XMP.Droid.Views.Splash;

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

        public void NavigateToDetails(MainViewModel fromViewModel, DetailsParameters detailsParameters)
        {
            var fromView = NavigationViewProvider.GetActivity<MainActivity, MainViewModel>(fromViewModel);

            Navigate<DetailsActivity, DetailsParameters>(fromView, detailsParameters);
        }

        public void NavigateToLogin(LauncherViewModel fromViewModel)
        {
            var fromView = NavigationViewProvider.GetActivity<SplashActivity, LauncherViewModel>(fromViewModel);

            Navigate<LoginActivity>(fromView);
        }

        public void NavigateToLogin()
        {
            var currentActivity = Application.CurrentActivity;

            if (currentActivity != null && !(currentActivity is LoginActivity))
            {
                var intent = new Intent(Application.Context, typeof(LoginActivity));

                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

                currentActivity.StartActivity(intent);

                currentActivity.Finish();
            }
        }

        public void NavigateToMain(LoginViewModel fromViewModel)
        {
            var fromView = NavigationViewProvider.GetActivity<LoginActivity, LoginViewModel>(fromViewModel);

            Navigate<MainActivity>(fromView);
        }

        public void NavigateToMain(LauncherViewModel fromViewModel)
        {
            var fromView = NavigationViewProvider.GetActivity<SplashActivity, LauncherViewModel>(fromViewModel);

            Navigate<MainActivity>(fromView);
        }
    }
}
