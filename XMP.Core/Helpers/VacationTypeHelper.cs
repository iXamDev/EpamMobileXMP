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
                case VacationType.Exceptional:
                    return "Exceptional Leave";

                case VacationType.WithoutPay:
                    return "Leave Without Pay";

                case VacationType.Overtime:
                    return "Overtime";

                case VacationType.Regular:
                    return "Regular";

                case VacationType.Sick:
                    return "Sick Days";
            }

            return null;
        }
    }
}
