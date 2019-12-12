using System;
using XMP.Core.Models;

namespace XMP.Core.Helpers
{
    public static class VacantionRequestFilterTypeHelper
    {
        public static string DisplayTitle(this VacantionRequestFilterType filter)
        {
            string Format(string s) => s.ToUpper();

            switch (filter)
            {
                case VacantionRequestFilterType.All:
                    return Format("all");

                case VacantionRequestFilterType.Open:
                    return Format("open");

                case VacantionRequestFilterType.Closed:
                    return Format("close");
            }

            return null;
        }
    }
}
