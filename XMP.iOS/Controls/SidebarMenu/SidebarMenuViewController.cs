using System;
using UIKit;
using CoreGraphics;
using SidebarNavigation;

namespace XMP.iOS.Controls.SidebarMenu
{
    public class SidebarMenuViewController : SidebarController
    {
        public SidebarMenuViewController(IntPtr handle) : base(handle)
        {
        }

        public SidebarMenuViewController(UIViewController rootViewController, UIViewController contentViewController, UIViewController menuViewController) : base(rootViewController, contentViewController, menuViewController)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _sidebar.ContentViewController.View.Frame = new CoreGraphics.CGRect(CGPoint.Empty, this.View.Bounds.Size);
        }
    }
}
