using System;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using XMP.Core.ValueConverters;
using XMP.Core.ViewModels.Details;
using XMP.Core.ViewModels.Details.Items;
using XMP.IOS.Bindings;
using XMP.IOS.ValueConverters;

namespace XMP.IOS.Views.Details.Cells
{
    public class DetailsItemCollectionViewCell : CollectionViewBindableItemCell<DetailsViewModel, DetailsItemVM>
    {
        public DetailsItemCollectionViewCell(IntPtr handle)
            : base(handle)
        {
        }

        public static string CellId { get; } = nameof(DetailsItemCollectionViewCell);

        public DetailsItemView View { get; set; }

        public override void LoadView()
        {
            View = new DetailsItemView();

            ContentView.AddSubview(View);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            ContentView.AddConstraints(View.FullSizeOf(ContentView));
        }

        public override void Bind(BindingSet<DetailsItemVM> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(View.TitleLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.Type)
                .WithConversion<VacationTypeTitleValueConverter>();

            bindingSet.Bind(View.ImageView)
                .For(v => v.BundleImageBinding())
                .To(vm => vm.Type)
                .WithConversion<VacationTypeToBundleImageNameValueConverter>();
        }
    }
}
