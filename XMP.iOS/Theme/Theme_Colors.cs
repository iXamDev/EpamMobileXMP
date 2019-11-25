using System;
using UIKit;
using Xamarin.Essentials;

namespace XMP.iOS
{
    public static partial class Theme
    {
        public static class Colors
        {
            public static UIColor Accent { get; } = ColorConverters.FromHex("#43C5D9").ToPlatformColor();

            public static UIColor NavbarTint { get; } = UIColor.White;

            public static UIColor AccentButtonTitle { get; } = ColorConverters.FromHex("#C6F1FF").ToPlatformColor();

            public static UIColor GrayText { get; } = ColorConverters.FromHex("#C6C6C6").ToPlatformColor();

            public static UIColor ErrorText { get; } = ColorConverters.FromHex("#932D54").ToPlatformColor();

            public static UIColor OverlayBackground { get; } = ColorConverters.FromHex("#F8F8F8").ToPlatformColor();

            public static UIColor MenuDevider { get; } = ColorConverters.FromHex("#CDCDCD").ToPlatformColor();

            public static UIColor MenuFooter { get; } = ColorConverters.FromHex("#CCCCCC").ToPlatformColor();
        }
    }
}