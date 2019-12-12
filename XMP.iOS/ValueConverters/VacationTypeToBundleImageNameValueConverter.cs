using System;
using System.Collections.Generic;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using XMP.Core.Models;
using XMP.Core.ValueConverters;

namespace XMP.iOS.ValueConverters
{
    public class VacationTypeToBundleImageNameValueConverter : DictionaryValueConverter<VacationType, string>
    {
        private Dictionary<VacationType, string> _mapping;

        public VacationTypeToBundleImageNameValueConverter()
        {
            _mapping = new Dictionary<VacationType, string>
            {
                { VacationType.Exceptional, "VacationExceptionalLeave" },
                { VacationType.WithoutPay, "VacationNotPayable" },
                { VacationType.Overtime, "VacationOvertime" },
                { VacationType.Regular, "VacationRegular" },
                { VacationType.Sick, "VacationSickDays" },
            };
        }

        protected override ConversionResult<string> Convert(VacationType value, Type targetType, object parameter, CultureInfo culture)
        => base.Convert(value, targetType, _mapping, culture);
    }
}
