using System;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views.Core;
using UIKit;
using XMP.Core.ViewModels.Main;

namespace XMP.iOS.Views.Main
{
    public class ContentHolderVC : UIViewController
    {
        public override void LoadView()
        {
            View = new UIView();

            View.BackgroundColor = UIColor.Yellow;
        }
    }
}
