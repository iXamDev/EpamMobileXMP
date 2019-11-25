using System;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;

namespace XMP.iOS.Views.Main
{
    public class MenuView : LayoutView
    {
        public UIImageView AvatarImageView { get; private set; }

        public UILabel NameLabel { get; private set; }

        public UITableView MenuTableView { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = UIColor.White;

            AvatarImageView = new UIImageView
            {
                ContentMode = UIViewContentMode.ScaleAspectFit
            };

            NameLabel = new UILabel().WithStyle(Theme.Text.MenuTitle);

            MenuTableView = new UITableView
            {
                BackgroundColor = Theme.Colors.MenuFooter
            };
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(AvatarImageView);

            this.AddLayoutSubview(NameLabel);

            this.AddLayoutSubview(MenuTableView);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints
            (
                AvatarImageView.AtTopOf(this, 32),
                AvatarImageView.WithSameCenterX(this),
                AvatarImageView.Width().EqualTo(114),
                AvatarImageView.Height().EqualTo(114),

                NameLabel.Below(AvatarImageView, 18),
                NameLabel.AtLeadingOf(this, 16),
                NameLabel.AtTrailingOf(this, 16),

                MenuTableView.Below(NameLabel, 14),
                MenuTableView.AtLeadingOf(this),
                MenuTableView.AtTrailingOf(this),
                MenuTableView.AtBottomOf(this)
            );
        }
    }
}
