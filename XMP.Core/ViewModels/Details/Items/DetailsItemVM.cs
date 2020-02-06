using FlexiMvvm.ViewModels;
using XMP.Core.Models;

namespace XMP.Core.ViewModels.Details.Items
{
    public class DetailsItemVM : ViewModel
    {
        public DetailsItemVM(VacationType vacationType)
        {
            Type = vacationType;
        }

        public VacationType Type { get; }
    }
}
