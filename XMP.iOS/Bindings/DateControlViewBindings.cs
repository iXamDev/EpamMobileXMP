using System;
using FlexiMvvm.Bindings;
using FlexiMvvm.Bindings.Custom;
using XMP.iOS.Controls;

namespace XMP.iOS.Bindings
{
    public static class DateControlViewBindings
    {
        public static TargetItemBinding<DateControlView, DateTime> DateBinding(
            this IItemReference<DateControlView> viewReference)
        {
            if (viewReference == null)
                throw new ArgumentNullException(nameof(viewReference));

            return new TargetItemOneWayCustomBinding<DateControlView, DateTime>(
                viewReference,
                (view, date) => view.Date = date,
                () => "Date");
        }
    }
}
