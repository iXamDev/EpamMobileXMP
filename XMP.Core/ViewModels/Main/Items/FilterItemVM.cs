using System;
using FlexiMvvm.ViewModels;

namespace XMP.Core.ViewModels.Main.Items
{
    public class FilterItemVM : ViewModel
    {
        public string Title { get; }

        public FilterItemVM(string title)
        {
            Title = title;
        }
    }
}
