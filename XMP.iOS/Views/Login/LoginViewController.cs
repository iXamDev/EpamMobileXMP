using System;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Login;
using UIKit;
using FlexiMvvm.Bindings;
using FlexiMvvm.ValueConverters;

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

            bindingSet.Bind(View.SignInButton)
                .For(v => v.SetTitleBinding())
                .To(vm => vm.LoginButtonTitle);

            bindingSet
                .Bind(View.LoginField)
                .For(v => v.TextAndEditingChangedBinding())
                .To(vm => vm.Login);

            bindingSet
                .Bind(View.PasswordField)
                .For(v => v.TextAndEditingChangedBinding())
                .To(vm => vm.Password);

            bindingSet
                .Bind(View.LoginField)
                .For(v => v.PlaceholderBinding())
                .To(vm => vm.LoginHint);

            bindingSet
                .Bind(View.PasswordField)
                .For(v => v.PlaceholderBinding())
                .To(vm => vm.PasswordHint);

            bindingSet
                .Bind(View.ErrorMessageView)
                .For(v => v.HiddenBinding())
                .To(vm => vm.ShowError)
                .WithConversion<InvertValueConverter>();

            bindingSet
                .Bind(View.ErrorMessageTriangleImage)
                .For(v => v.HiddenBinding())
                .To(vm => vm.ShowError)
                .WithConversion<InvertValueConverter>();

            bindingSet
                .Bind(View.ErrorLabel)
                .For(v => v.TextBinding())
                .To(vm => vm.ErrorMessage);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController?.SetNavigationBarHidden(false, animated);
        }
    }
}
