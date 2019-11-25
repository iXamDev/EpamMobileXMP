using System;
using FlexiMvvm.Bindings;
using UIKit;
using XMP.Core.ViewModels.Main;

namespace XMP.iOS.Views.Main
{
    public class MenuHolderVC : UIViewController
    {
        public new MenuView View
        {
            get => (MenuView)base.View;
            set => base.View = value;
        }

        public override void LoadView()
        {
            View = new MenuView();
        }

        public void Bind(BindingSet<MainViewModel> bindingSet)
        {
            bindingSet
                .Bind(View.NameLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.UserName);

            View.AvatarImageView.Image = UIImage.FromBundle("UserAvatar");
        }
    }
}
