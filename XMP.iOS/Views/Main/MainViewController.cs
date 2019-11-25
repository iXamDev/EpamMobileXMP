using System;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using SidebarNavigation;
using XMP.Core.ViewModels.Main;
using UIKit;
using System.Threading.Tasks;
using XMP.iOS.Extensions;
using FlexiMvvm.ViewModels;

namespace XMP.iOS.Views.Main
{
    public class MainViewController : BindableViewController<MainViewModel>
    {
        private UIButton navbarAddButton;
        private UILabel navbarTitleLabel;

        public SidebarController SidebarController { get; private set; }

        protected ContentHolderVC ContentHolder { get; private set; }

        protected MenuHolderVC MenuHolder { get; private set; }

        public MainViewController()
        {
        }

        private void OnCloseMenuInteraction(object sender, EventArgs e)
        {
            if (SidebarController?.IsOpen ?? false)
                SidebarController.ToggleMenu();
        }

        private void OnMenuClick(object sender, EventArgs e)
        => SidebarController.ToggleMenu();

        private SidebarController CreateSidebarMenu()
        {
            var sidebarController = new SidebarController(this, ContentHolder, MenuHolder);

            sidebarController.HasShadowing = false;
            sidebarController.MenuWidth = Theme.Dimensions.SideMenuWidth;
            sidebarController.MenuLocation = MenuLocations.Left;

            return sidebarController;
        }

        public override void Bind(BindingSet<MainViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(navbarAddButton)
                .For(v => v.SetTitleBinding())
                .To(vm => vm.AddButtonTitle);

            bindingSet.Bind(navbarAddButton)
                .For(v => v.TouchUpInsideBinding())
                .To(vm => vm.AddCmd);

            bindingSet.Bind(navbarTitleLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.ScreenTitle);

            ViewModel.CloseMenuInteraction.RequestedWeakSubscribe(OnCloseMenuInteraction);

            ContentHolder.Bind(bindingSet);

            MenuHolder.Bind(bindingSet);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var menuButton = new UIBarButtonItem(UIImage.FromBundle("Menu"), UIBarButtonItemStyle.Plain, null);

            menuButton.ClickedWeakSubscribe(OnMenuClick);

            NavigationItem.LeftBarButtonItem = menuButton;

            navbarAddButton = new UIButton(UIButtonType.Custom)
            {
                ImageEdgeInsets = new UIEdgeInsets(8, 0, 8, 0),
                BackgroundColor = UIColor.Clear,
                SemanticContentAttribute = UISemanticContentAttribute.ForceRightToLeft
            }
            .WithImageForAllStates(UIImage.FromBundle("Plus"));

            navbarAddButton.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;

            NavigationItem.RightBarButtonItem = new UIBarButtonItem(navbarAddButton);

            NavigationItem.CreateAndSetScreenTitleLabel(out navbarTitleLabel);
        }

        public override void LoadView()
        {
            base.LoadView();

            ContentHolder = new ContentHolderVC();

            MenuHolder = new MenuHolderVC();
            SidebarController = CreateSidebarMenu();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController?.SetNavigationBarHidden(false, animated);
        }
    }
}