using System;
using FlexiMvvm.Bindings;
using Android.Support.V7.Widget;
using FlexiMvvm.Bindings.Custom;

namespace XMP.Droid.Bindings
{
    public static class ToolbarBindings
    {
        public static TargetItemBinding<Toolbar, string> TitleBinding(
            this IItemReference<Toolbar> viewReference)
        {
            if (viewReference == null)
                throw new ArgumentNullException(nameof(viewReference));

            return new TargetItemOneWayCustomBinding<Toolbar, string>(
                viewReference,
                (view, title) => view.Title = title,
                () => "Title");
        }
    }
}
