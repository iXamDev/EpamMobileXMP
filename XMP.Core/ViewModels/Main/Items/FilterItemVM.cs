using System;
using FlexiMvvm.ViewModels;
using XMP.Core.Models;
using XMP.Core.Helpers;

namespace XMP.Core.ViewModels.Main.Items
{
    public class FilterItemVM : ViewModel
    {
        public FilterItemVM(VacantionRequestFilterType type)
        {
            Type = type;

            Title = Type.DisplayTitle();
        }

        public string Title { get; }

        public VacantionRequestFilterType Type { get; }
    }
}
