using FlexiMvvm.Views;
using UIKit;
using XMP.Core.ViewModels.Launcher;

namespace XMP.iOS.Views.Splash
{
    public class SplashViewController : ViewController<LauncherViewModel>
    {
        public override void LoadView()
        {
            View = new UIImageView(UIImage.FromBundle("SplashBackground"));
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController?.SetNavigationBarHidden(true, animated);
        }
    }
}
