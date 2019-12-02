using System;
using Android.Widget;
using FlexiMvvm.Bindings;
using FlexiMvvm.Bindings.Custom;

namespace XMP.Droid.Bindings
{
    public static class ImageBindings
    {
        public static TargetItemBinding<ImageView, int> ImageResourceBinding(
            this IItemReference<ImageView> viewReference)
        {
            if (viewReference == null)
                throw new ArgumentNullException(nameof(viewReference));

            return new TargetItemOneWayCustomBinding<ImageView, int>(
                viewReference,
                (view, res) => view.SetImageResource(res),
                () => "SetImageResource");
        }
    }
}
