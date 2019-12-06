using System;
using System.Collections.Generic;

namespace XMP.Core.Helpers
{
    public static class EnumHelper
    {
        public static T[] GetEnumStates<T>()
        {
            var varians = Enum.GetValues(typeof(T));

            var newItems = new List<T>();

            foreach (var item in varians)
                newItems.Add((T)item);

            return newItems.ToArray();
        }
    }
}
