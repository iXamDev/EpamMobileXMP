using System;
using Cirrious.FluentLayouts.Touch;
using CoreGraphics;
using FlexiMvvm.Views;
using UIKit;
using XMP.iOS.Controls;
using XMP.iOS.Views.Details.Cells;

namespace XMP.iOS.Views.Details
{
    public class DetailsView : LayoutView
    {
        private UIView _topDevider;

        private UIView _bottomDevider;

        public UICollectionView CollectionView { get; private set; }

        public DateControlView StartDateControlView { get; private set; }

        public DateControlView EndDateControlView { get; private set; }

        public UISegmentedControl StateSegmentedControl { get; private set; }

        public UIPageControl PageControl { get; private set; }

        private nfloat CollectionHeight => 188;

        protected override void SetupSubviews()
        {
            base.SetupSubviews();

            BackgroundColor = Theme.Colors.ScreenBackground;

            CollectionView = new UICollectionView(
                CGRect.Empty,
                new UICollectionViewFlowLayout
                {
                    ScrollDirection = UICollectionViewScrollDirection.Horizontal,
                    ItemSize = new CGSize(UIScreen.MainScreen.Bounds.Width, CollectionHeight),
                    MinimumLineSpacing = 0,
                    MinimumInteritemSpacing = 0
                })
            {
                PagingEnabled = true,
                BackgroundColor = Theme.Colors.ScreenBackground,
                ShowsHorizontalScrollIndicator = false
            };
            CollectionView.RegisterClassForCell(typeof(DetailsItemCollectionViewCell), DetailsItemCollectionViewCell.CellId);

            _topDevider = new UIView { BackgroundColor = Theme.Colors.Accent };

            _bottomDevider = new UIView { BackgroundColor = Theme.Colors.Accent };

            StartDateControlView = new DateControlView
            {
                TextColor = Theme.Colors.Accent
            };

            EndDateControlView = new DateControlView
            {
                TextColor = Theme.Colors.Green
            };

            PageControl = new UIPageControl
            {
                CurrentPageIndicatorTintColor = Theme.Colors.Accent,
                PageIndicatorTintColor = Theme.Colors.Gray,
                UserInteractionEnabled = false
            };

            StateSegmentedControl = new UISegmentedControl()
            {
                SelectedSegmentTintColor = Theme.Colors.Green
            };
            StateSegmentedControl.SetTitleTextAttributes(
                new UITextAttributes
                {
                    TextColor = Theme.Colors.Gray
                }, UIControlState.Normal);
            StateSegmentedControl.SetTitleTextAttributes(
                new UITextAttributes
                {
                    TextColor = UIColor.White
                }, UIControlState.Selected);
            StateSegmentedControl.SetTitleTextAttributes(
                new UITextAttributes
                {
                    TextColor = UIColor.White
                }, UIControlState.Highlighted);
        }

        protected override void SetupLayout()
        {
            base.SetupLayout();

            this.AddLayoutSubview(CollectionView);

            this.AddLayoutSubview(PageControl);

            this.AddLayoutSubview(_topDevider);

            this.AddLayoutSubview(StartDateControlView);

            this.AddLayoutSubview(EndDateControlView);

            this.AddLayoutSubview(_bottomDevider);

            this.AddLayoutSubview(StateSegmentedControl);
        }

        protected override void SetupLayoutConstraints()
        {
            base.SetupLayoutConstraints();

            this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.AddConstraints(
                CollectionView.AtTopOf(this),
                CollectionView.AtLeadingOf(this),
                CollectionView.AtTrailingOf(this),
                CollectionView.Height().EqualTo(CollectionHeight),
                PageControl.AtBottomOf(CollectionView, 25),
                PageControl.WithSameCenterX(this),
                PageControl.Height().EqualTo(8),
                _topDevider.Below(CollectionView),
                _topDevider.AtLeadingOf(this),
                _topDevider.AtTrailingOf(this),
                _topDevider.Height().EqualTo(Theme.Dimensions.DeviderWidth),
                StartDateControlView.Below(_topDevider, 6),
                StartDateControlView.AtLeadingOf(this, 10),
                EndDateControlView.WithSameTop(StartDateControlView),
                EndDateControlView.AtTrailingOf(this, 12),
                _bottomDevider.Below(StartDateControlView, 4),
                _bottomDevider.AtLeadingOf(this),
                _bottomDevider.AtTrailingOf(this),
                _bottomDevider.Height().EqualTo(Theme.Dimensions.DeviderWidth),
                StateSegmentedControl.Below(_bottomDevider, 32),
                StateSegmentedControl.WithSameCenterX(this));
        }
    }
}
