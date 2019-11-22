using System;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Details;
using UIKit;
namespace XMP.iOS.Views.Details
{
    public class DetailsViewController : BindableViewController<DetailsViewModel>
    {
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

            NavigationItem.RightBarButtonItem = new UIBarButtonItem("Save", UIBarButtonItemStyle.Plain, HandleEventHandler);
        }

        void HandleEventHandler(object sender, EventArgs e)
        {
            ViewModel?.SaveCmd.Execute(null);
        }

    }
}
