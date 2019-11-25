using FlexiMvvm.Bindings;
using UIKit;
using XMP.Core.ViewModels.Main;

namespace XMP.iOS.Views.Main
{
    public class ContentHolderVC : UIViewController
    {
        public new ContentView View
        {
            get => (ContentView)base.View;
            set => base.View = value;
        }

        public override void LoadView()
        {
            View = new ContentView();
        }

        public void Bind(BindingSet<MainViewModel> bindingSet)
        {

        }
    }
}
