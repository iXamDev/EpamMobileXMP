using System;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using XMP.Core.ViewModels.Main;
using XMP.Core.ViewModels.Main.Items;
using XMP.Core.ValueConverters;
using XMP.iOS.Bindings;
using XMP.iOS.ValueConverters;

namespace XMP.iOS.Views.Main.Cells
{
    public class ContentVacationRequestItemTableViewCell : TableViewBindableItemCell<MainViewModel, VacationRequestItemVM>
    {
        public ContentVacationRequestItemTableViewCell(IntPtr handle) : base(handle)
        {
            SelectionStyle = UIKit.UITableViewCellSelectionStyle.None;
        }

        public static string CellId { get; } = nameof(ContentVacationRequestItemTableViewCell);

        public ContentVacationRequestItemView View { get; set; }

        public override void LoadView()
        {
            View = new ContentVacationRequestItemView();

            ContentView.AddSubview(View);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            ContentView.AddConstraints(View.FullSizeOf(ContentView));
        }

        public override void Bind(BindingSet<VacationRequestItemVM> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(View.RangeLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.Range);

            bindingSet.Bind(View.TypeLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.Type)
                .WithConversion<VacationTypeTitleValueConverter>();

            bindingSet.Bind(View.StateLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.State)
                .WithConversion<VacationStateTitleValueConverter>();

            bindingSet.Bind(View.IconImageView)
                .For(v => v.BundleImageBinding())
                .To(vm => vm.Type)
                .WithConversion<VacationTypeToBundleImageNameValueConverter>();
        }
    }
}
