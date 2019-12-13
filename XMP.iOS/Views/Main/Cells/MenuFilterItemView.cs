using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;

namespace XMP.IOS.Views.Main.Cells
{
    public class MenuFilterItemView : LayoutView
    {
        public UILabel TitleLabel { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            TitleLabel = new UILabel().WithStyle(Theme.Text.MenuItem);

            BackgroundColor = Theme.Colors.MenuItemBackground;
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(TitleLabel);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                TitleLabel.AtTrailingOf(this, 30),
                TitleLabel.WithSameCenterY(this));
        }
    }
}
