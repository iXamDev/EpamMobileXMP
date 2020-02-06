using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Views;
using UIKit;

namespace XMP.IOS.Views.Main.Cells
{
    public class ContentVacationRequestItemView : LayoutView
    {
        public UILabel RangeLabel { get; private set; }

        public UILabel TypeLabel { get; private set; }

        public UILabel StateLabel { get; private set; }

        public UIImageView IconImageView { get; private set; }

        public UIImageView ArrowImageView { get; private set; }

        public UIView MainDeviderView { get; private set; }

        public UIView LastItemDeviderView { get; private set; }

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            RangeLabel = new UILabel().WithStyle(Theme.Text.MenuItem);

            TypeLabel = new UILabel().WithStyle(Theme.Text.GrayText);

            StateLabel = new UILabel().WithStyle(Theme.Text.HugeGrayText);

            MainDeviderView = new UIView { BackgroundColor = Theme.Colors.MenuDevider };

            LastItemDeviderView = new UIView { BackgroundColor = Theme.Colors.MenuDevider };

            IconImageView = new UIImageView
            {
                ContentMode = UIViewContentMode.ScaleAspectFit
            };

            ArrowImageView = new UIImageView(UIImage.FromBundle("Arrow"));

            BackgroundColor = UIColor.White;
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(RangeLabel);

            this.AddLayoutSubview(IconImageView);

            this.AddLayoutSubview(TypeLabel);

            this.AddLayoutSubview(StateLabel);

            this.AddLayoutSubview(ArrowImageView);

            this.AddLayoutSubview(MainDeviderView);

            this.AddLayoutSubview(LastItemDeviderView);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                IconImageView.AtLeadingOf(this, 13),
                IconImageView.WithSameCenterY(this),
                IconImageView.Width().EqualTo(36),
                IconImageView.Height().EqualTo(36),
                RangeLabel.ToTrailingOf(IconImageView, 16),
                RangeLabel.AtTopOf(this, 16),
                TypeLabel.AtLeadingOf(RangeLabel),
                TypeLabel.Below(RangeLabel),
                ArrowImageView.WithSameCenterY(this),
                ArrowImageView.AtTrailingOf(this, 12),
                ArrowImageView.Width().EqualTo(8),
                ArrowImageView.Height().EqualTo(13),
                StateLabel.ToLeadingOf(ArrowImageView, 6),
                StateLabel.WithSameCenterY(this),
                MainDeviderView.Height().EqualTo(Theme.Dimensions.DeviderWidth),
                MainDeviderView.AtBottomOf(this),
                MainDeviderView.AtTrailingOf(this),
                MainDeviderView.WithSameLeading(RangeLabel),
                LastItemDeviderView.WithSameHeight(MainDeviderView),
                LastItemDeviderView.AtBottomOf(this),
                LastItemDeviderView.AtLeadingOf(this),
                LastItemDeviderView.ToLeadingOf(MainDeviderView));
        }
    }
}
