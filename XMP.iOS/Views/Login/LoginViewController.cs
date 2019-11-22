using System;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Login;
using UIKit;
using FlexiMvvm.Bindings;

namespace XMP.iOS.Views.Login
{
    public class LoginViewController : BindableViewController<LoginViewModel>
    {
        public new LoginView View
        {
            get => (LoginView)base.View;
            set => base.View = value;
        }

        public LoginViewController()
        {
        }

        public override void LoadView()
        {
            View = new LoginView();
        }

        public override void Bind(BindingSet<LoginViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet.Bind(View.SignInButton)
                .For(v => v.TouchUpInsideBinding())
                .To(vm => vm.LoginCmd);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController?.SetNavigationBarHidden(false, animated);
        }
    }
}
