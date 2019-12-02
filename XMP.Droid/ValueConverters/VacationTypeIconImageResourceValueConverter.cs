using System;
using System.Collections.Generic;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using XMP.Core.Models;
using XMP.Core.ValueConverters;

namespace XMP.Droid.ValueConverters
{
    public class VacationTypeIconImageResourceValueConverter : DictionaryValueConverter<VacationType, int>
    {
        private Dictionary<VacationType, int> mapping;

        public VacationTypeIconImageResourceValueConverter()
        {
            mapping = new Dictionary<VacationType, int>
            {
                { VacationType.Exceptional, Resource.Drawable.ic_vacation_exceptional },
                { VacationType.WithoutPay, Resource.Drawable.ic_vacation_without_pay},
                { VacationType.Overtime, Resource.Drawable.ic_vacation_overtime },
                { VacationType.Regular, Resource.Drawable.ic_vacation_regular },
                { VacationType.Sick, Resource.Drawable.ic_vacation_sick },
            };
        }

        protected override ConversionResult<int> Convert(VacationType value, Type targetType, object parameter, CultureInfo culture)
        => base.Convert(value, targetType, mapping, culture);
    }
}
