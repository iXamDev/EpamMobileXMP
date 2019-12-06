using System;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Details;
using UIKit;
using FlexiMvvm.Bindings;
using XMP.iOS.Extensions;
using XMP.iOS.Bindings;
using FlexiMvvm.Collections;
using XMP.iOS.Views.Details.Cells;
using XMP.iOS.Views.Details.Source;
using XMP.Core.ValueConverters;
using XMP.Core.Models;
using System.Linq;
using XMP.Core.Helpers;
using System.Collections.Generic;

namespace XMP.iOS.Views.Details
{
    public class DetailsViewController : BindableViewController<DetailsViewModel, DetailsParameters>
    {
        private UILabel navbarTitleLabel;

        private DetailsItemsSource itemsSource;

        private Dictionary<VacationState, nint> stateSegmentedControlMapping;

        public new DetailsView View
        {
            get => (DetailsView)base.View;
            set => base.View = value;
        }

        public override void LoadView()
        {
            View = new DetailsView();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.RightBarButtonItem = new UIBarButtonItem("Save", UIBarButtonItemStyle.Plain, null);

            NavigationItem.CreateAndSetScreenTitleLabel(out navbarTitleLabel);

            itemsSource = new DetailsItemsSource(View.CollectionView, View.PageControl)
            {
                ItemsContext = ViewModel
            };

            View.CollectionView.Source = itemsSource;

            stateSegmentedControlMapping = View.StateSegmentedControl.SetupSegmentsMapping(ViewModel.AvailableVacationStates, state => state.DisplayTitle());
        }

        public override void Bind(BindingSet<DetailsViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet
                .Bind(navbarTitleLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.ScreenTitle);

            bindingSet
                .Bind(View.StartDateControlView)
                .For(v => v.DateBinding())
                .To(vm => vm.StartDate);

            bindingSet
                .Bind(View.StartDateControlView)
                .For(v => v.TapBinding())
                .To(vm => vm.ShowStartDateDialogCmd);

            bindingSet
                .Bind(View.EndDateControlView)
                .For(v => v.DateBinding())
                .To(vm => vm.EndDate);

            bindingSet
                .Bind(View.EndDateControlView)
                .For(v => v.TapBinding())
                .To(vm => vm.ShowEndDateDialogCmd);

            bindingSet
                .Bind(itemsSource)
                .For(v => v.ItemsBinding())
                .To(vm => vm.VacationTypeItems);

            bindingSet
                .Bind(itemsSource)
                .For(v => v.FocusedItemBinding())
                .To(vm => vm.SelectedVacationType);

            bindingSet.Bind(View.StateSegmentedControl)
                .For(v => v.SelectedSegmentAndValueChangedBinding())
                .To(vm => vm.VacationState)
                .WithConversion<DictionaryValueConverter<VacationState, nint>>(stateSegmentedControlMapping)
                .WithFallbackValue(-1);

            bindingSet.Bind(NavigationItem.RightBarButtonItem)
                .For(v => v.ClickedBinding())
                .To(vm => vm.SaveCmd);
        }
    }
}
