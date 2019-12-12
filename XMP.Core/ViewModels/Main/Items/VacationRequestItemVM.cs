using System;
using FlexiMvvm.ViewModels;
using XMP.Core.Models;

namespace XMP.Core.ViewModels.Main.Items
{
    public class VacationRequestItemVM : ViewModel
    {
        private string _range;

        private VacationType _type;

        private VacationState _state;

        public string Range
        {
            get => _range;
            private set => SetValue(ref _range, value, nameof(Range));
        }

        public VacationType Type
        {
            get => _type;
            private set => SetValue(ref _type, value, nameof(Type));
        }

        public VacationState State
        {
            get => _state;
            private set => SetValue(ref _state, value, nameof(State));
        }

        public VacantionRequest Model { get; private set; }

        public void SetModel(VacantionRequest model)
        {
            Model = model;

            string FormatDate(DateTimeOffset date) => $"{date.ToString("MMM").ToUpper()} {date.ToString("dd")}";

            Range = $"{FormatDate(model.Start)} - {FormatDate(model.End)}";

            State = model.State;

            Type = model.VacationType;
        }
    }
}
