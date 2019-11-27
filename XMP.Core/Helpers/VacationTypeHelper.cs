using System;
using XMP.Core.Models;
namespace XMP.Core.Helpers
{
    public static class VacationTypeHelper
    {
        public static string DisplayTitle(this VacationType vacationType)
        {
            switch (vacationType)
            {
                case VacationType.ExceptionalLeave:
                    return "Exceptional Leave";

                case VacationType.NotPayable:
                    return "Not Payable";

                case VacationType.Overtime:
                    return "Overtime";

                case VacationType.Regular:
                    return "Regular";

                case VacationType.SickDays:
                    return "Sick Days";
            }

            return null;
        }
    }
}
