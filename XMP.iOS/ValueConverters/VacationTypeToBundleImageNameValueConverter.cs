using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using XMP.Core.Models;
using XMP.Core.ValueConverters;
using System.Collections.Generic;
namespace XMP.iOS.ValueConverters
{
    public class VacationTypeToBundleImageNameValueConverter : DictionaryValueConverter<VacationType, string>
    {
        private Dictionary<VacationType, string> mapping;

        public VacationTypeToBundleImageNameValueConverter()
        {
            mapping = new Dictionary<VacationType, string>
            {
                { VacationType.Exceptional, "VacationExceptionalLeave" },
                { VacationType.WithoutPay, "VacationNotPayable" },
                { VacationType.Overtime, "VacationOvertime" },
                { VacationType.Regular, "VacationRegular" },
                { VacationType.Sick, "VacationSickDays" },
            };
        }

        protected override ConversionResult<string> Convert(VacationType value, Type targetType, object parameter, CultureInfo culture)
        => base.Convert(value, targetType, mapping, culture);
    }
}
