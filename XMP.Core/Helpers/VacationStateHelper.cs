using System;
using XMP.Core.Models;
namespace XMP.Core.Helpers
{
    public static class VacationStateHelper
    {
        public static string DisplayTitle(this VacationState state)
        {
            switch (state)
            {
                case VacationState.Approved:
                    return "Approved";

                case VacationState.Closed:
                    return "Closed";
            }

            return null;
        }
    }
}
