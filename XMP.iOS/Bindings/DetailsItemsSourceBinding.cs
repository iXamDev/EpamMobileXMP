using System;
using FlexiMvvm.Bindings;
using XMP.iOS.Views.Details.Source;
using XMP.Core.ViewModels.Details.Items;
using FlexiMvvm.Bindings.Custom;

namespace XMP.iOS.Bindings
{
    public static class DetailsItemsSourceBinding
    {
        public static TargetItemBinding<DetailsItemsSource, DetailsItemVM> FocusedItemBinding(
            this IItemReference<DetailsItemsSource> sourceReference)
        {
            if (sourceReference == null)
                throw new ArgumentNullException(nameof(sourceReference));

            return new TargetItemTwoWayCustomBinding<DetailsItemsSource, DetailsItemVM>(
                sourceReference,
                (source, eventHandler) => source.FocusedItemChanged += eventHandler,
                (source, eventHandler) => source.FocusedItemChanged -= eventHandler,
                (source, cmd) => { },
                (source) => source.FocusedItem,
                (source, item) => source.FocusedItem = item,
                () => "FocusedItem");
        }
    }
}
