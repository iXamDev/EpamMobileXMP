using System;
using System.Collections.Generic;
using FlexiMvvm.Bindings;
using FlexiMvvm.Bindings.Custom;
using XMP.Droid.Adapters;
using XMP.Droid.Views.Common;

namespace XMP.Droid.Bindings
{
    public static class BindableViewPagerStateAdapterBindings
    {
        public static TargetItemBinding<BindableViewPagerStateAdapter<TViewModel, TFragment>, IEnumerable<TViewModel>> ItemsBinding<TViewModel, TFragment>(
        this IItemReference<BindableViewPagerStateAdapter<TViewModel, TFragment>> viewReference)
            where TViewModel : class
            where TFragment : BindableViewPagerFragment<TViewModel>
        {
            if (viewReference == null)
                throw new ArgumentNullException(nameof(viewReference));

            return new TargetItemOneWayCustomBinding<BindableViewPagerStateAdapter<TViewModel, TFragment>, IEnumerable<TViewModel>>(
                viewReference,
                (adapter, items) => adapter.Items = items,
                () => "Items");
        }
    }
}
