using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FlexiMvvm.ValueConverters;

namespace XMP.Core.ValueConverters
{
    public class DictionaryValueConverter<TSource, TValue> : ValueConverter<TSource, TValue>
    {
        protected override ConversionResult<TValue> Convert(TSource value, Type targetType, object parameter, CultureInfo culture)
        {
            var dictionary = GetMapping(parameter);

            if (dictionary.ContainsKey(value))
                return ConversionResult<TValue>.SetValue(dictionary[value]);
            else
                return ConversionResult<TValue>.UnsetValue();
        }

        protected override ConversionResult<TSource> ConvertBack(TValue value, Type targetType, object parameter, CultureInfo culture)
        {
            var dictionary = GetMapping(parameter);

            if (dictionary.ContainsValue(value))
                return ConversionResult<TSource>.SetValue(dictionary.FirstOrDefault(x => x.Value.Equals(value)).Key);
            else
                return ConversionResult<TSource>.UnsetValue();
        }

        private Dictionary<TSource, TValue> GetMapping(object parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter), $"Parameter should be of type \"{typeof(Dictionary<TSource, TValue>)}\" and must contain mapping definition");

            if (parameter is Dictionary<TSource, TValue> dictionary)
                return dictionary;
            else
                throw new ArgumentException($"Dictionary value converter expects a parameter of type \"{typeof(Dictionary<TSource, TValue>)}\" but received type \"{parameter.GetType()}\"", nameof(parameter));
        }
    }
}
