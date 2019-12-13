using System;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;

namespace XMP.IOS.Views.Details.Cells
{
    public class DetailsItemView : LayoutView
    {
        public UIImageView ImageView { get; private set; }

        public UILabel TitleLabel { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            ImageView = new UIImageView
            {
                BackgroundColor = UIColor.Clear,
                ContentMode = UIViewContentMode.ScaleToFill
            };

            TitleLabel = new UILabel().WithStyle(Theme.Text.GrayText.WithTune(t => t.Font = UIFont.SystemFontOfSize(44)));
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(ImageView);

            this.AddLayoutSubview(TitleLabel);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                ImageView.Width().EqualTo(74),
                ImageView.Height().EqualTo(74),
                ImageView.AtTopOf(this, 15),
                ImageView.WithSameCenterX(this),
                TitleLabel.Below(ImageView, 8),
                TitleLabel.WithSameCenterX(this));
        }
    }
}
