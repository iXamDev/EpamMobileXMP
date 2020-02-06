using UIKit;
using XMP.IOS.Extensions;

namespace XMP.IOS
{
    public static partial class Theme
    {
        public enum ButtonStyle
        {
            Accent
        }

        public static UIButton WithStyle(this UIButton button, ButtonStyle style)
        {
            switch (style)
            {
                case ButtonStyle.Accent:
                    Buttons.SetupAccentButton(button);
                    break;
            }

            return button;
        }

        public static class Buttons
        {
            public static void SetupAccentButton(UIButton button)
            {
                button.BackgroundColor = Colors.Accent;

                button
                    .WithTitleColorForAllStates(Colors.AccentButtonTitle);
            }
        }
    }
}
