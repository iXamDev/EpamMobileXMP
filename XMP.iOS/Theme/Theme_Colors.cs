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

        }
    }
}