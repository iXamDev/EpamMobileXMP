using System;
using UIKit;
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
    }
}
