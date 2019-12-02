using System;
using Android.Support.V4.View;
using Android.Support.V4.App;
using FlexiMvvm.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using FlexiMvvm;
using XMP.Droid.Views.Common;
using Android.Views;
using Java.Lang;

namespace XMP.Droid.Adapters
{
    public class BindableViewPagerStateAdapter<TViewModel, TFragment> : FragmentStatePagerAdapter
        where TViewModel : class
        where TFragment : BindableViewPagerFragment<TViewModel>
    {
        public BindableViewPagerStateAdapter(FragmentManager fm, Func<TViewModel, TFragment> fragmentsCreator) : base(fm)
        {
            FragmentsCreator = fragmentsCreator;
        }

        private DisposableCollection itemsSubscriptions;

        private Func<TViewModel, TFragment> FragmentsCreator { get; }

        private IEnumerable<TViewModel> items;
        public IEnumerable<TViewModel> Items
        {
            get => items;
            set
            {
                if (!ReferenceEquals(items, value))
                {
                    items = value;

                    RefreshItemsSubscriptions();
                    Reload(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }

        private DisposableCollection ItemsSubscriptions => itemsSubscriptions ?? (itemsSubscriptions = new DisposableCollection());

        public override int Count => Items?.Count() ?? 0;

        private void RefreshItemsSubscriptions()
        {
            itemsSubscriptions?.Dispose();
            itemsSubscriptions = null;

            if (Items is INotifyCollectionChanged observableItems)
            {
                observableItems.CollectionChangedWeakSubscribe(Items_CollectionChanged).DisposeWith(ItemsSubscriptions);
            }
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Reload(e);
        }

        protected virtual void Reload(NotifyCollectionChangedEventArgs args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            NotifyDataSetChanged();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                itemsSubscriptions?.Dispose();
                itemsSubscriptions = null;
            }

            base.Dispose(disposing);
        }

        protected virtual object GetItemDataContext(int position)
        {
            return Items.ElementAtOrDefault(position);
        }

        public override Fragment GetItem(int position)
        {
            var vm = GetItemDataContext(position) as TViewModel;

            var fragment = FragmentsCreator(vm);

            fragment.ViewModel = vm;

            return fragment;
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            var baseFragment = base.InstantiateItem(container, position);

            if (baseFragment is TFragment fragment && fragment.ViewModel == null)
            {
                fragment.ViewModel = GetItemDataContext(position) as TViewModel;

                fragment.BindingSet.SetSourceItem(fragment.ViewModel);
            }

            return baseFragment;
        }
    }
}
