using System;
using UIKit;

namespace XMP.iOS
{
    public static partial class Theme
    {
        public static UILabel WithStyle(this UILabel label, UILabelStyle style)
        {
            if (style.BackgroundColor != null)
                label.BackgroundColor = style.BackgroundColor;

            if (style.Font != null)
                label.Font = style.Font;

            if (style.TextColor != null)
                label.TextColor = style.TextColor;

            if (style.TextAlignment.HasValue)
                label.TextAlignment = style.TextAlignment.Value;

            if (style.Lines != null)
                label.Lines = style.Lines.Value;

            if (style.LineBreakMode.HasValue)
                label.LineBreakMode = style.LineBreakMode.Value;

            return label;
        }

        public static UILabelStyle WithTune(this UILabelStyle labelStyle, Action<UILabelStyle> tune)
        {
            tune(labelStyle);

            return labelStyle;
        }

        public static class Text
        {
            public static UILabelStyle NavbarTitle => new UILabelStyle
            {
                TextColor = UIColor.White,
                Font = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold),
                Lines = 1
            };

            public static UILabelStyle MenuTitle => new UILabelStyle
            {
                TextColor = Theme.Colors.Accent,
                Font = UIFont.SystemFontOfSize(24, UIFontWeight.Semibold),
                Lines = 1,
                LineBreakMode = UILineBreakMode.MiddleTruncation,
                TextAlignment = UITextAlignment.Center
            };

            public static UILabelStyle MenuItem => new UILabelStyle
            {
                TextColor = Theme.Colors.DarkGrayText,
                Font = UIFont.SystemFontOfSize(20, UIFontWeight.Bold),
                Lines = 1,
            };

            public static UILabelStyle AccentText => new UILabelStyle
            {
                TextColor = Theme.Colors.Accent,
                Font = UIFont.SystemFontOfSize(22, UIFontWeight.Semibold),
                Lines = 1,
            };

            public static UILabelStyle GrayText => new UILabelStyle
            {
                TextColor = Theme.Colors.DarkGrayText,
                Font = UIFont.SystemFontOfSize(13, UIFontWeight.Regular),
                Lines = 1,
            };

            public static UILabelStyle HugeGrayText
                => GrayText
                .WithTune(s => s.Font = UIFont.SystemFontOfSize(15));
        }

        public class UILabelStyle
        {
            public UIColor BackgroundColor { get; set; }

            public UIColor TextColor { get; set; }

            public UITextAlignment? TextAlignment { get; set; }

            public UIFont Font { get; set; }

            public int? Lines { get; set; }

            public UILineBreakMode? LineBreakMode { get; set; }
        }
    }
}
