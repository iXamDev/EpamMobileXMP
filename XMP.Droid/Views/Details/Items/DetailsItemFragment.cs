using Android.OS;
using Android.Views;
using FlexiMvvm.Bindings;
using XMP.Core.ValueConverters;
using XMP.Core.ViewModels.Details.Items;
using XMP.Droid.Bindings;
using XMP.Droid.ValueConverters;
using XMP.Droid.Views.Common;

namespace XMP.Droid.Views.Details.Items
{
    public class DetailsItemFragment : BindableViewPagerFragment<DetailsItemVM>
    {
        private DetailsItemFragmentViewHolder ViewHolder { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_details_item, container, false);

            ViewHolder = new DetailsItemFragmentViewHolder(view);

            return view;
        }

        public override void Bind(BindingSet<DetailsItemVM> bindingSet)
        {
            bindingSet
                .Bind(ViewHolder.TitleText)
                .For(v => v.TextBinding())
                .To(vm => vm.Type)
                .WithConversion<VacationTypeTitleValueConverter>();

            bindingSet
                .Bind(ViewHolder.IconImage)
                .For(v => v.ImageResourceBinding())
                .To(vm => vm.Type)
                .WithConversion<VacationTypeIconImageResourceValueConverter>();
        }
    }
}
