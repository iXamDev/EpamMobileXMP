using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Android.Support.V4.App;
using Android.Views;
using FlexiMvvm;
using FlexiMvvm.Collections;
using XMP.Droid.Views.Common;

namespace XMP.Droid.Adapters
{
    public class BindableViewPagerStateAdapter<TViewModel, TFragment> : FragmentStatePagerAdapter
        where TViewModel : class
        where TFragment : BindableViewPagerFragment<TViewModel>
    {
        private DisposableCollection _itemsSubscriptions;

        private IEnumerable<TViewModel> _items;

        public BindableViewPagerStateAdapter(FragmentManager fm, Func<TViewModel, TFragment> fragmentsCreator)
            : base(fm)
        {
            FragmentsCreator = fragmentsCreator;
        }

        public override int Count => Items?.Count() ?? 0;

        public IEnumerable<TViewModel> Items
        {
            get => _items;
            set
            {
                if (!ReferenceEquals(_items, value))
                {
                    _items = value;

                    RefreshItemsSubscriptions();
                    Reload(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
            }
        }

        private Func<TViewModel, TFragment> FragmentsCreator { get; }

        private DisposableCollection ItemsSubscriptions => _itemsSubscriptions ?? (_itemsSubscriptions = new DisposableCollection());

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
                _itemsSubscriptions?.Dispose();
                _itemsSubscriptions = null;
            }

            base.Dispose(disposing);
        }

        protected virtual object GetItemDataContext(int position)
        {
            return Items.ElementAtOrDefault(position);
        }

        private void RefreshItemsSubscriptions()
        {
            _itemsSubscriptions?.Dispose();
            _itemsSubscriptions = null;

            if (Items is INotifyCollectionChanged observableItems)
            {
                observableItems.CollectionChangedWeakSubscribe(Items_CollectionChanged).DisposeWith(ItemsSubscriptions);
            }
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Reload(e);
        }
    }
}
