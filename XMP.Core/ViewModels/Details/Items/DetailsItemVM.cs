using System;
using FlexiMvvm.ViewModels;
using XMP.Core.Models;
namespace XMP.Core.ViewModels.Details.Items
{
    public class DetailsItemVM : ViewModel
    {
        public VacationType Type { get; }

        public DetailsItemVM(VacationType vacationType)
        {
            Type = vacationType;
        }
    }
}
