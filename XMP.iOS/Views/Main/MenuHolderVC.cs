using FlexiMvvm.Bindings;
using FlexiMvvm.Collections;
using UIKit;
using XMP.Core.ViewModels.Main;
using XMP.IOS.Views.Main.Cells;

namespace XMP.IOS.Views.Main
{
    public class MenuHolderVC : UIViewController
    {
        public new MenuView View
        {
            get => (MenuView)base.View;
            set => base.View = value;
        }

        private TableViewObservablePlainSource MenuFilterSource { get; set; }

        public override void LoadView()
        {
            View = new MenuView();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            MenuFilterSource = new TableViewObservablePlainSource(View.MenuTableView, vm => MenuFilterItemTableViewCell.CellId);

            View.MenuTableView.Source = MenuFilterSource;

            View.AvatarImageView.Image = UIImage.FromBundle("UserAvatar");
        }

        public void Bind(BindingSet<MainViewModel> bindingSet)
        {
            bindingSet
                .Bind(View.NameLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.UserName);

            bindingSet.Bind(MenuFilterSource)
                .For(v => v.ItemsBinding())
                .To(vm => vm.FilterItems);

            bindingSet.Bind(MenuFilterSource)
                .For(v => v.RowSelectedBinding())
                .To(vm => vm.FilterCmd);
        }
    }
}
