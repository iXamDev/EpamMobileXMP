using System;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Launcher;
using UIKit;

namespace XMP.iOS.Views.Splash
{
    public class SplashViewController : ViewController<LauncherViewModel>
    {
        public override void LoadView()
        {
            View = new UIImageView(UIImage.FromBundle("SplashBackground"));
        }
    }
}

