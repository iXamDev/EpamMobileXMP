using Android.Views;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using XMP.Core.ValueConverters;
using XMP.Core.ViewModels.Main;
using XMP.Core.ViewModels.Main.Items;
using XMP.Droid.Bindings;
using XMP.Droid.ValueConverters;

namespace XMP.Droid.Views
{
    public partial class MainRequestCellViewHolder : RecyclerViewBindableItemViewHolder<MainViewModel, VacationRequestItemVM>
    {
        public MainRequestCellViewHolder(View itemView)
            : base(itemView)
        {
        }

        public override void Bind(BindingSet<VacationRequestItemVM> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet
                .Bind(IconImage)
                .For(v => v.ImageResourceBinding())
                .To(vm => vm.Type)
                .WithConversion<VacationTypeIconImageResourceValueConverter>();

            bindingSet
                .Bind(RangeText)
                .For(v => v.TextBinding())
                .To(vm => vm.Range);

            bindingSet
                .Bind(TypeText)
                .For(v => v.TextBinding())
                .To(vm => vm.Type)
                .WithConversion<VacationTypeTitleValueConverter>();

            bindingSet
                .Bind(StateText)
                .For(v => v.TextBinding())
                .To(vm => vm.State)
                .WithConversion<VacationStateTitleValueConverter>();
        }
    }
}
