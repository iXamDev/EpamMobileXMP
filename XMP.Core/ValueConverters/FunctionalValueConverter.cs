using System;
using System.Globalization;
using FlexiMvvm.ValueConverters;

namespace XMP.Core.ValueConverters
{
    public class FunctionalValueConverter<TSource, TValue> : ValueConverter<TSource, TValue>
    {
        protected override ConversionResult<TValue> Convert(TSource value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConversionResult<TValue>.SetValue(GetMapping(parameter).Convert.Invoke(value));
        }

        protected override ConversionResult<TSource> ConvertBack(TValue value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConversionResult<TSource>.SetValue(GetMapping(parameter).ConvertBack.Invoke(value));
        }

        private FunctionalValueParameter<TSource, TValue> GetMapping(object parameter)
        => parameter as FunctionalValueParameter<TSource, TValue>;
    }

    public class FunctionalValueParameter<TSource, TValue>
    {
        public FunctionalValueParameter(Func<TSource, TValue> convert, Func<TValue, TSource> convertBack)
        {
            Convert = convert;

            ConvertBack = convertBack;
        }

        public Func<TSource, TValue> Convert { get; }

        public Func<TValue, TSource> ConvertBack { get; }
    }
}
