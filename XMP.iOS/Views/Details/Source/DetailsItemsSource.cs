using System;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using FlexiMvvm.Collections;
using JetBrains.Annotations;
using UIKit;
using XMP.iOS.Views.Details.Cells;
using XMP.Core.ViewModels.Details.Items;
using System.Collections.Generic;
using Foundation;
using CoreGraphics;

namespace XMP.iOS.Views.Details.Source
{
    public class DetailsItemsSource : CollectionViewObservablePlainSource
    {
        public DetailsItemsSource(UICollectionView collectionView, UIPageControl pageControl) : base(collectionView, _ => DetailsItemCollectionViewCell.CellId)
        {
            pageControlReference = new WeakReference<UIPageControl>(pageControl);
        }

        private bool ignoreFocusedItemChange;

        private bool shouldSetInitialPosition = true;

        private int lastPage = -1;

        private WeakReference<UIPageControl> pageControlReference;

        public bool AnimateFocusedItemChange { get; set; }

        public event EventHandler FocusedItemChanged;

        private DetailsItemVM focusedItem;
        public DetailsItemVM FocusedItem
        {
            get => focusedItem;
            set
            {
                if (!ReferenceEquals(focusedItem, value))
                {
                    focusedItem = value;

                    ScrollToItem(value);
                }
            }
        }

        public new List<DetailsItemVM> Items
        {
            get => base.Items as List<DetailsItemVM>;
            set => base.Items = value;
        }

        protected nfloat ItemWidth(UICollectionView collectionView) => (collectionView.CollectionViewLayout as UICollectionViewFlowLayout)?.ItemSize.Width ?? 0;

        public override void Scrolled(UIScrollView scrollView)
        {
            var currentPage = GetCurrentPageIndex(scrollView as UICollectionView);

            if (lastPage != currentPage)
            {
                lastPage = currentPage;

                ScrolledToPage(currentPage);
            }
        }

        public override void WillDisplayCell(UICollectionView collectionView, UICollectionViewCell cell, NSIndexPath indexPath)
        {
            if (shouldSetInitialPosition)
            {
                shouldSetInitialPosition = false;

                ScrollToItem(FocusedItem);
            }
        }

        public override void ScrollAnimationEnded(UIScrollView scrollView)
        => SetSettingAdjustingScrollPosition(false);

        protected void RaiseFocusedItemChanged()
        => FocusedItemChanged?.Invoke(this, EventArgs.Empty);

        protected override void ReloadSections(NotifyCollectionChangedEventArgs args)
        {
            UIPageControl pageControl = null;

            if (pageControlReference.TryGetTarget(out var control))
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
            if (!ignoreFocusedItemChange)
            {
                var newFocusedItem = Items?.ElementAtOrDefault(page);

                if (!ReferenceEquals(focusedItem, newFocusedItem))
                {
                    focusedItem = newFocusedItem;

                    RaiseFocusedItemChanged();
                }
            }

            if (pageControlReference.TryGetTarget(out var pageControl))
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
            ignoreFocusedItemChange = executing;

            if (CollectionViewWeakReference.TryGetTarget(out var collectionView))
                collectionView.ScrollEnabled = !executing;
        }
    }
}
