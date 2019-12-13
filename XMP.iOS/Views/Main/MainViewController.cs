using System;
using FlexiMvvm.Bindings;
using FlexiMvvm.ViewModels;
using FlexiMvvm.Views;
using SidebarNavigation;
using UIKit;
using XMP.Core.ViewModels.Main;
using XMP.IOS.Extensions;
using XMP.IOS.Views.SidebarMenu;

namespace XMP.IOS.Views.Main
{
    public class MainViewController : BindableViewController<MainViewModel>
    {
        private UIButton _navbarAddButton;

        private UILabel _navbarTitleLabel;

        public MainViewController()
        {
        }

        public SidebarMenuViewController SidebarControllerVC { get; private set; }

        protected ContentHolderVC ContentHolder { get; private set; }

        protected MenuHolderVC MenuHolder { get; private set; }

        public override void Bind(BindingSet<MainViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(_navbarAddButton)
                .For(v => v.SetTitleBinding())
                .To(vm => vm.AddButtonTitle);

            bindingSet.Bind(_navbarAddButton)
                .For(v => v.TouchUpInsideBinding())
                .To(vm => vm.AddCmd);

            bindingSet.Bind(_navbarTitleLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.ScreenTitle);

            ViewModel.CloseMenuInteraction.RequestedWeakSubscribe(OnCloseMenuInteraction);

            ContentHolder.Bind(bindingSet, ViewModel);

            MenuHolder.Bind(bindingSet);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var menuButton = new UIBarButtonItem(UIImage.FromBundle("Menu"), UIBarButtonItemStyle.Plain, null);

            menuButton.ClickedWeakSubscribe(OnMenuClick);

            NavigationItem.LeftBarButtonItem = menuButton;

            _navbarAddButton = new UIButton(UIButtonType.Custom)
            {
                ImageEdgeInsets = new UIEdgeInsets(8, 0, 8, 0),
                BackgroundColor = UIColor.Clear,
                SemanticContentAttribute = UISemanticContentAttribute.ForceRightToLeft
            }
            .WithImageForAllStates(UIImage.FromBundle("Plus"));

            _navbarAddButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(_navbarAddButton);

            NavigationItem.CreateAndSetScreenTitleLabel(out _navbarTitleLabel);
        }

        public override void LoadView()
        {
            base.LoadView();

            ContentHolder = new ContentHolderVC();

            MenuHolder = new MenuHolderVC();

            SidebarControllerVC = CreateSidebarMenu();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController?.SetNavigationBarHidden(false, animated);
        }

        private void OnCloseMenuInteraction(object sender, EventArgs e)
        {
            if (SidebarControllerVC?.IsOpen ?? false)
                SidebarControllerVC.ToggleMenu();
        }

        private void OnMenuClick(object sender, EventArgs e)
        => SidebarControllerVC.ToggleMenu();

        private SidebarMenuViewController CreateSidebarMenu()
        {
            var sidebarController = new SidebarMenuViewController(this, ContentHolder, MenuHolder);

            sidebarController.HasShadowing = false;
            sidebarController.MenuWidth = Theme.Dimensions.SideMenuWidth;
            sidebarController.MenuLocation = MenuLocations.Left;

            return sidebarController;
        }
    }
}
