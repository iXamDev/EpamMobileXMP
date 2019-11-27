using System;
using FlexiMvvm.ViewModels;
using XMP.Core.Models;
namespace XMP.Core.ViewModels.Main.Items
{
    public class VacationRequestItemVM : ViewModel
    {
        private string range;
        public string Range
        {
            get => range;
            private set => SetValue(ref range, value, nameof(Range));
        }

        private VacationType type;
        public VacationType Type
        {
            get => type;
            private set => SetValue(ref type, value, nameof(Type));
        }

        private VacationState state;
        public VacationState State
        {
            get => state;
            private set => SetValue(ref state, value, nameof(State));
        }

        public VacantionRequest Model { get; private set; }

        public void SetModel(VacantionRequest model)
        {
            Model = model;

            string FormatDate(DateTime date) => date.ToString("MMM dd");

            Range = $"{FormatDate(model.StartDate)} - {FormatDate(model.EndDate)}";

            State = model.State;

            Type = model.VacationType;
        }
    }
}
