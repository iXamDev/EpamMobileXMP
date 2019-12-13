using System;
using CoreGraphics;
using SidebarNavigation;
using UIKit;

namespace XMP.IOS.Views.SidebarMenu
{
    public class SidebarMenuViewController : SidebarController
    {
        public SidebarMenuViewController(IntPtr handle)
            : base(handle)
        {
        }

        public SidebarMenuViewController(UIViewController rootViewController, UIViewController contentViewController, UIViewController menuViewController)
            : base(rootViewController, contentViewController, menuViewController)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _sidebar.ContentViewController.View.Frame = new CoreGraphics.CGRect(CGPoint.Empty, View.Bounds.Size);
        }
    }
}
