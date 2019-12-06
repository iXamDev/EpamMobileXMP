using System;
using System.Collections.Generic;
using FlexiMvvm;
using System.Linq;
namespace XMP.Core.Mapping
{
    public class EnumMapping<T, V>
    {
        private readonly Dictionary<T, V> mapping;

        private readonly T defaultT;

        private readonly V defaultV;

        public EnumMapping(Dictionary<T, V> mapping, T defaultT, V defaultV)
        {
            this.mapping = mapping;

            this.defaultT = defaultT;

            this.defaultV = defaultV;
        }

        public T Get(V value)
        => mapping.ContainsValue(value) ? mapping.First(t => t.Value.Equals(value)).Key : defaultT;

        public V Get(T key)
        => mapping.GetValueOrDefault(key, defaultV);
    }
}
