using System;
using FlexiMvvm.Bindings;
using FlexiMvvm.Bindings.Custom;
using XMP.Droid.Controls;

namespace XMP.Droid.Bindings
{
    public static class DateControlLayoutBindings
    {
        public static TargetItemBinding<DateControlLayout, DateTime> DateBinding(
            this IItemReference<DateControlLayout> viewReference)
        {
            if (viewReference == null)
                throw new ArgumentNullException(nameof(viewReference));

            return new TargetItemOneWayCustomBinding<DateControlLayout, DateTime>(
                viewReference,
                (layout, date) => layout.Date = date,
                () => "Date");
        }
    }
}
