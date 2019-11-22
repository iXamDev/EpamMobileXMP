using System;
using UIKit;

namespace XMP.iOS
{
    public static partial class Theme
    {
        public static void SetupGrobalStyle()
        {
            SetupNavigationBar();
        }

        private static void SetupNavigationBar()
        {
            UINavigationBar.Appearance.ShadowImage = new UIImage();

            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.TintColor = Theme.Colors.NavbarTint;
            UINavigationBar.Appearance.BarTintColor = Theme.Colors.Accent;
            UINavigationBar.Appearance.BackgroundColor = Theme.Colors.Accent;
        }
    }
}
