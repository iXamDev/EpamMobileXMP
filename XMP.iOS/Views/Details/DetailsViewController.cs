using System;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Details;
using UIKit;
using FlexiMvvm.Bindings;
using XMP.iOS.Extensions;

namespace XMP.iOS.Views.Details
{
    public class DetailsViewController : BindableViewController<DetailsViewModel>
    {
        private UILabel navbarTitleLabel;

        public DetailsViewController()
        {

        }

        public override void LoadView()
        {
            View = new UIKit.UIView();

            View.BackgroundColor = UIColor.Brown;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.RightBarButtonItem = new UIBarButtonItem("Save", UIBarButtonItemStyle.Plain, null);

            NavigationItem.RightBarButtonItem.ClickedWeakSubscribe(HandleEventHandler);

            NavigationItem.CreateAndSetScreenTitleLabel(out navbarTitleLabel);
        }

        public override void Bind(BindingSet<DetailsViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(navbarTitleLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.ScreenTitle);
        }

        void HandleEventHandler(object sender, EventArgs e)
        {
            ViewModel?.SaveCmd.Execute(null);
        }
    }
}
