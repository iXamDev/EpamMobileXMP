using System;
using System.Collections.Generic;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using NN.Ios.Core.Attributes;
using UIKit;
using XMP.Core.Helpers;
using XMP.Core.Models;
using XMP.Core.ValueConverters;
using XMP.Core.ViewModels.Details;
using XMP.IOS.Bindings;
using XMP.IOS.Extensions;
using XMP.IOS.Views.Details.Source;

namespace XMP.IOS.Views.Details
{
    [PushPresentation]
    public class DetailsViewController : BindableViewController<DetailsViewModel, DetailsParameters>
    {
        private UILabel _navbarTitleLabel;

        private DetailsItemsSource _itemsSource;

        private Dictionary<VacationState, nint> _stateSegmentedControlMapping;

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

            NavigationItem.CreateAndSetScreenTitleLabel(out _navbarTitleLabel);

            _itemsSource = new DetailsItemsSource(View.CollectionView, View.PageControl)
            {
                ItemsContext = ViewModel
            };

            View.CollectionView.Source = _itemsSource;

            _stateSegmentedControlMapping = View.StateSegmentedControl.SetupSegmentsMapping(ViewModel.AvailableVacationStates, state => state.DisplayTitle());
        }

        public override void Bind(BindingSet<DetailsViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet
                .Bind(_navbarTitleLabel)
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
                .Bind(_itemsSource)
                .For(v => v.ItemsBinding())
                .To(vm => vm.VacationTypeItems);

            bindingSet
                .Bind(_itemsSource)
                .For(v => v.FocusedItemBinding())
                .To(vm => vm.SelectedVacationType);

            bindingSet.Bind(View.StateSegmentedControl)
                .For(v => v.SelectedSegmentAndValueChangedBinding())
                .To(vm => vm.VacationState)
                .WithConversion<DictionaryValueConverter<VacationState, nint>>(_stateSegmentedControlMapping)
                .WithFallbackValue(-1);

            bindingSet.Bind(NavigationItem.RightBarButtonItem)
                .For(v => v.ClickedBinding())
                .To(vm => vm.SaveCmd);
        }
    }
}
