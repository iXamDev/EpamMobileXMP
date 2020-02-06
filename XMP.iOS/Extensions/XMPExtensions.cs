using UIKit;

namespace XMP.IOS.Extensions
{
    public static class XMPExtensions
    {
        public static void CreateAndSetScreenTitleLabel(this UINavigationItem navigationItem, out UILabel titleLabel)
        {
            titleLabel = new UILabel().WithStyle(Theme.Text.NavbarTitle);
            titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            navigationItem.TitleView = titleLabel;
        }
    }
}
