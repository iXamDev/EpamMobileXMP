using System;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using SidebarNavigation;
using XMP.Core.ViewModels.Main;
namespace XMP.iOS.Views.Main
{
    public class MainViewController : BindableViewController<MainViewModel>
    {
        public SidebarController SidebarController { get; private set; }

        protected ContentHolderVC ContentHolder { get; private set; }

        protected MenuHolderVC MenuHolder { get; private set; }

        public MainViewController()
        {
        }

        public override void Bind(BindingSet<MainViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(MenuHolder.View.SignInButton)
                .For(v => v.TouchUpInsideBinding())
                .To(vm => vm.TestCmd);
        }

        public override void LoadView()
        {
            base.LoadView();

            ContentHolder = new ContentHolderVC();

            MenuHolder = new MenuHolderVC();

            SidebarController = new SidebarController(this, ContentHolder, MenuHolder);

            SidebarController.HasShadowing = false;
            SidebarController.MenuWidth = Theme.Dimensions.SideMenuWidth;
            SidebarController.MenuLocation = MenuLocations.Left;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController?.SetNavigationBarHidden(false, animated);
        }
    }
}
