using System;
using Cirrious.FluentLayouts.Touch;
using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using XMP.Core.ViewModels.Main;
using XMP.Core.ViewModels.Main.Items;

namespace XMP.iOS.Views.Main.Cells
{
    public class MenuFilterItemTableViewCell : TableViewBindableItemCell<MainViewModel, FilterItemVM>
    {
        public MenuFilterItemTableViewCell(IntPtr handle)
            : base(handle)
        {
        }

        public static string CellId { get; } = nameof(MenuFilterItemTableViewCell);

        public MenuFilterItemView View { get; set; }

        public override void LoadView()
        {
            View = new MenuFilterItemView();

            ContentView.AddSubview(View);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            ContentView.AddConstraints(View.FullSizeOf(ContentView));

            SelectionStyle = UIKit.UITableViewCellSelectionStyle.None;
        }

        public override void Bind(BindingSet<FilterItemVM> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(View.TitleLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.Title);
        }
    }
}
