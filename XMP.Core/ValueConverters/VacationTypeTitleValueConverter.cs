﻿using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;
using XMP.Core.Models;
using XMP.Core.Helpers;
namespace XMP.Core.ValueConverters
{
    public class VacationTypeTitleValueConverter : ValueConverter<VacationType, string>
    {
        protected override ConversionResult<string> Convert(VacationType value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueTitle = value.DisplayTitle();

            return valueTitle != null ? ConversionResult<string>.SetValue(valueTitle) : ConversionResult<string>.UnsetValue();
        }
    }
}
