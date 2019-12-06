using System;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Details;
using Android.App;
using Android.OS;
using Android.Views;
using FlexiMvvm.Bindings;
using XMP.Droid.Bindings;
using XMP.Core.ValueConverters;
using XMP.Core.Models;
using System.Collections.Generic;
using FlexiMvvm.Collections;
using XMP.Droid.Views.Details.Items;
using XMP.Droid.Adapters;
using XMP.Core.ViewModels.Details.Items;
using System.Linq;

namespace XMP.Droid.Views.Details
{
    [Activity]
    public class DetailsActivity : BindableAppCompatActivity<DetailsViewModel, DetailsParameters>
    {
        private DetailsActivityViewHolder ViewHolder { get; set; }

        private BindableViewPagerStateAdapter<DetailsItemVM, DetailsItemFragment> itemsAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.RequestedOrientation = Android.Content.PM.ScreenOrientation.Sensor;
            SetContentView(Resource.Layout.activity_details);

            ViewHolder = new DetailsActivityViewHolder(this);

            itemsAdapter = new BindableViewPagerStateAdapter<DetailsItemVM, DetailsItemFragment>(SupportFragmentManager, (arg) => new DetailsItemFragment());

            ViewHolder.Viewpager.Adapter = itemsAdapter;

            ViewHolder.PagingTabLayout.SetupWithViewPager(ViewHolder.Viewpager, true);
        }

        public override void Bind(BindingSet<DetailsViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet
                .Bind(itemsAdapter)
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
                .WithConversion<DictionaryValueConverter<VacationState, bool>>
                (
                    new Dictionary<VacationState, bool>
                    {
                        { VacationState.Approved, true }
                    }
                ).WithFallbackValue(false);

            bindingSet
                .Bind(ViewHolder.ClosedRadioButton)
                .For(v => v.CheckedAndCheckedChangeBinding())
                .To(vm => vm.VacationState)
                .WithConversion<DictionaryValueConverter<VacationState, bool>>
                (
                    new Dictionary<VacationState, bool>
                    {
                        { VacationState.Closed, true}
                    }
                ).WithFallbackValue(false);

            bindingSet
                .Bind(ViewHolder.Viewpager)
                .For(v => v.SetCurrentItemAndPageSelectedBinding())
                .To(vm => vm.SelectedVacationType)
                .WithConversion<FunctionalValueConverter<DetailsItemVM, int>>(new FunctionalValueParameter<DetailsItemVM, int>
                (
                    itemVM => ViewModel.VacationTypeItems.IndexOf(itemVM),
                    (index) => ViewModel.VacationTypeItems.ElementAt(index)
                ));
        }
    }
}
