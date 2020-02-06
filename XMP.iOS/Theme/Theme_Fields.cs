using UIKit;

namespace XMP.IOS
{
    public static partial class Theme
    {
        public static UITextField WithStyle(this UITextField textField)
        {
            textField.BackgroundColor = UIColor.White;
            textField.BorderStyle = UITextBorderStyle.None;
            textField.TintColor = Theme.Colors.GrayText;

            return textField;
        }
    }
}
