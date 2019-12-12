using System.Collections.Generic;
using System.Linq;
using FlexiMvvm;

namespace XMP.Core.Mapping
{
    public class EnumMapping<TFirst, TSecond>
    {
        private readonly Dictionary<TFirst, TSecond> _mapping;

        private readonly TFirst _defaultT;

        private readonly TSecond _defaultV;

        public EnumMapping(Dictionary<TFirst, TSecond> mapping, TFirst defaultT, TSecond defaultV)
        {
            _mapping = mapping;

            _defaultT = defaultT;

            _defaultV = defaultV;
        }

        public TFirst Get(TSecond value)
        => _mapping.ContainsValue(value) ? _mapping.First(t => t.Value.Equals(value)).Key : _defaultT;

        public TSecond Get(TFirst key)
        => _mapping.GetValueOrDefault(key, _defaultV);
    }
}
