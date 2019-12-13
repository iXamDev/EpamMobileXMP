using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using FlexiMvvm.Collections;
using Foundation;
using UIKit;
using XMP.Core.ViewModels.Details.Items;
using XMP.iOS.Views.Details.Cells;

namespace XMP.iOS.Views.Details.Source
{
    public class DetailsItemsSource : CollectionViewObservablePlainSource
    {
        private bool _ignoreFocusedItemChange;

        private bool _shouldSetInitialPosition = true;

        private int _lastPage = -1;

        private DetailsItemVM _focusedItem;

        private WeakReference<UIPageControl> _pageControlReference;

        public DetailsItemsSource(UICollectionView collectionView, UIPageControl pageControl)
            : base(collectionView, _ => DetailsItemCollectionViewCell.CellId)
        {
            _pageControlReference = new WeakReference<UIPageControl>(pageControl);
        }

        public event EventHandler FocusedItemChanged;

        public bool AnimateFocusedItemChange { get; set; }

        public DetailsItemVM FocusedItem
        {
            get => _focusedItem;
            set
            {
                if (!ReferenceEquals(_focusedItem, value))
                {
                    _focusedItem = value;

                    ScrollToItem(value);
                }
            }
        }

        public new List<DetailsItemVM> Items
        {
            get => base.Items as List<DetailsItemVM>;
            set => base.Items = value;
        }

        public override void Scrolled(UIScrollView scrollView)
        {
            var currentPage = GetCurrentPageIndex(scrollView as UICollectionView);

            if (_lastPage != currentPage)
            {
                _lastPage = currentPage;

                ScrolledToPage(currentPage);
            }
        }

        public override void WillDisplayCell(UICollectionView collectionView, UICollectionViewCell cell, NSIndexPath indexPath)
        {
            if (_shouldSetInitialPosition)
            {
                _shouldSetInitialPosition = false;

                ScrollToItem(FocusedItem);
            }
        }

        public override void ScrollAnimationEnded(UIScrollView scrollView)
        => SetSettingAdjustingScrollPosition(false);

        protected nfloat ItemWidth(UICollectionView collectionView) => (collectionView.CollectionViewLayout as UICollectionViewFlowLayout)?.ItemSize.Width ?? 0;

        protected void RaiseFocusedItemChanged()
        => FocusedItemChanged?.Invoke(this, EventArgs.Empty);

        protected override void ReloadSections(NotifyCollectionChangedEventArgs args)
        {
            UIPageControl pageControl = null;

            if (_pageControlReference.TryGetTarget(out var control))
            {
                pageControl = control;

                pageControl.Pages = Items?.Count() ?? 0;
            }

            base.ReloadSections(args);

            if (pageControl != null && CollectionViewWeakReference.TryGetTarget(out var collectionView))
            {
                var currentPage = GetCurrentPageIndex(collectionView);

                pageControl.CurrentPage = currentPage;
            }
        }

        private int GetCurrentPageIndex(UICollectionView collectionView)
        {
            var itemWidth = ItemWidth(collectionView);

            var centerOffsetPoint = (float)(collectionView.Frame.Width - itemWidth) / 2f;

            var currentPageCenterPosition = collectionView.ContentOffset.X + centerOffsetPoint;

            return (int)Math.Round(currentPageCenterPosition / itemWidth);
        }

        private void ScrolledToPage(int page)
        {
            if (!_ignoreFocusedItemChange)
            {
                var newFocusedItem = Items?.ElementAtOrDefault(page);

                if (!ReferenceEquals(_focusedItem, newFocusedItem))
                {
                    _focusedItem = newFocusedItem;

                    RaiseFocusedItemChanged();
                }
            }

            if (_pageControlReference.TryGetTarget(out var pageControl))
                pageControl.CurrentPage = page;
        }

        private void ScrollToItem(DetailsItemVM value)
        {
            if (value == null)
                return;

            var index = Items?.IndexOf(value) ?? -1;

            if (index >= 0 && CollectionViewWeakReference.TryGetTarget(out var collectionView))
            {
                if (AnimateFocusedItemChange)
                    SetSettingAdjustingScrollPosition(true);

                collectionView.ScrollToItem(NSIndexPath.FromRowSection(index, DefaultSection), UICollectionViewScrollPosition.CenteredHorizontally, AnimateFocusedItemChange);
            }
        }

        private void SetSettingAdjustingScrollPosition(bool executing)
        {
            _ignoreFocusedItemChange = executing;

            if (CollectionViewWeakReference.TryGetTarget(out var collectionView))
                collectionView.ScrollEnabled = !executing;
        }
    }
}
