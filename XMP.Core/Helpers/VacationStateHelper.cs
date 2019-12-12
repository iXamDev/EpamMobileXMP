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
                case VacationState.Draft:
                    return "Draft";

                case VacationState.Submitted:
                    return "Submitted";

                case VacationState.Approved:
                    return "Approved";

                case VacationState.InProgress:
                    return "InProgress";

                case VacationState.Closed:
                    return "Closed";
            }

            return null;
        }
    }
}
