using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;
using XMP.IOS.Views.Main.Cells;

namespace XMP.IOS.Views.Main
{
    public class MenuView : LayoutView
    {
        public UIImageView AvatarImageView { get; private set; }

        public UILabel NameLabel { get; private set; }

        public UITableView MenuTableView { get; private set; }

        public UIView MenuDeviderView { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = Theme.Colors.ScreenBackground;

            AvatarImageView = new UIImageView
            {
                ContentMode = UIViewContentMode.ScaleAspectFit
            };

            NameLabel = new UILabel().WithStyle(Theme.Text.MenuTitle);

            MenuTableView = new UITableView
            {
                BackgroundColor = Theme.Colors.MenuFooter,
                RowHeight = Theme.Dimensions.MenuItemHeight,
                SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine,
                SeparatorInset = UIEdgeInsets.Zero,
                SeparatorColor = Theme.Colors.MenuDevider,
                Bounces = false
            };
            MenuTableView.RegisterClassForCellReuse(typeof(MenuFilterItemTableViewCell), MenuFilterItemTableViewCell.CellId);

            MenuDeviderView = new UIView
            {
                BackgroundColor = Theme.Colors.MenuDevider
            };
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(AvatarImageView);

            this.AddLayoutSubview(NameLabel);

            this.AddLayoutSubview(MenuDeviderView);

            this.AddLayoutSubview(MenuTableView);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                AvatarImageView.AtTopOf(this, 32),
                AvatarImageView.WithSameCenterX(this),
                AvatarImageView.Width().EqualTo(114),
                AvatarImageView.Height().EqualTo(114),
                NameLabel.Below(AvatarImageView, 18),
                NameLabel.AtLeadingOf(this, 16),
                NameLabel.AtTrailingOf(this, 16),
                MenuDeviderView.AtLeadingOf(this, 0),
                MenuDeviderView.AtTrailingOf(this, 0),
                MenuDeviderView.Height().EqualTo(Theme.Dimensions.DeviderWidth),
                MenuDeviderView.Below(NameLabel, 14),
                MenuTableView.Below(MenuDeviderView),
                MenuTableView.AtLeadingOf(this),
                MenuTableView.AtTrailingOf(this),
                MenuTableView.AtBottomOf(this));
        }
    }
}
