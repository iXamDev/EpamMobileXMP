using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using XMP.Core.Models;
using XMP.Core.ValueConverters;
using XMP.Core.ViewModels.Details;
using XMP.Core.ViewModels.Details.Items;
using XMP.Droid.Adapters;
using XMP.Droid.Bindings;
using XMP.Droid.Views.Details.Items;

namespace XMP.Droid.Views.Details
{
    [Activity]
    public class DetailsActivity : BindableAppCompatActivity<DetailsViewModel, DetailsParameters>
    {
        private BindableViewPagerStateAdapter<DetailsItemVM, DetailsItemFragment> _itemsAdapter;

        private DetailsActivityViewHolder ViewHolder { get; set; }

        public override void Bind(BindingSet<DetailsViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet
                .Bind(_itemsAdapter)
                .For(v => v.ItemsBinding())
                .To(vm => vm.VacationTypeItems);

            bindingSet
                .Bind(ViewHolder.ToolbarTitle)
                .For(v => v.TextBinding())
                .To(vm => vm.ScreenTitle);

            bindingSet
                .Bind(ViewHolder.SaveButton)
                .For(v => v.ClickBinding())
                .To(vm => vm.SaveCmd);

            bindingSet
                .Bind(ViewHolder.StartDate)
                .For(v => v.DateBinding())
                .To(vm => vm.StartDate);

            bindingSet
              .Bind(ViewHolder.StartDate)
              .For(v => v.ClickBinding())
              .To(vm => vm.ShowStartDateDialogCmd);

            bindingSet
                .Bind(ViewHolder.EndDate)
                .For(v => v.DateBinding())
                .To(vm => vm.EndDate);

            bindingSet
                .Bind(ViewHolder.EndDate)
                .For(v => v.ClickBinding())
                .To(vm => vm.ShowEndDateDialogCmd);

            bindingSet
                .Bind(ViewHolder.ApprovedRadioButton)
                .For(v => v.CheckedAndCheckedChangeBinding())
                .To(vm => vm.VacationState)
                .WithConversion<DictionaryValueConverter<VacationState, bool>>(
                    new Dictionary<VacationState, bool>
                    {
                        { VacationState.Approved, true }
                    })
                .WithFallbackValue(false);

            bindingSet
                .Bind(ViewHolder.ClosedRadioButton)
                .For(v => v.CheckedAndCheckedChangeBinding())
                .To(vm => vm.VacationState)
                .WithConversion<DictionaryValueConverter<VacationState, bool>>(
                    new Dictionary<VacationState, bool>
                    {
                        { VacationState.Closed, true }
                    })
                .WithFallbackValue(false);

            bindingSet
                .Bind(ViewHolder.Viewpager)
                .For(v => v.SetCurrentItemAndPageSelectedBinding())
                .To(vm => vm.SelectedVacationType)
                .WithConversion<FunctionalValueConverter<DetailsItemVM, int>>(new FunctionalValueParameter<DetailsItemVM, int>(
                    itemVM => ViewModel.VacationTypeItems.IndexOf(itemVM),
                    (index) => ViewModel.VacationTypeItems.ElementAt(index)));
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestedOrientation = Android.Content.PM.ScreenOrientation.Sensor;
            SetContentView(Resource.Layout.activity_details);

            ViewHolder = new DetailsActivityViewHolder(this);

            _itemsAdapter = new BindableViewPagerStateAdapter<DetailsItemVM, DetailsItemFragment>(SupportFragmentManager, (arg) => new DetailsItemFragment());

            ViewHolder.Viewpager.Adapter = _itemsAdapter;

            ViewHolder.PagingTabLayout.SetupWithViewPager(ViewHolder.Viewpager, true);
        }
    }
}
