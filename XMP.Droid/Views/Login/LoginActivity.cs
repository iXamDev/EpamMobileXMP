using Android.App;
using Android.OS;
using FlexiMvvm.Bindings;
using FlexiMvvm.ValueConverters;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Login;

namespace XMP.Droid.Views.Login
{
    [Activity(NoHistory = true)]
    public class LoginActivity : BindableAppCompatActivity<LoginViewModel>
    {
        private LoginActivityViewHolder ViewHolder { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);

            ViewHolder = new LoginActivityViewHolder(this);
        }

        public override void Bind(BindingSet<LoginViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet
                .Bind(ViewHolder.LoginEdit)
                .For(v => v.TextAndTextChangedBinding())
                .To(vm => vm.Login);

            bindingSet
                .Bind(ViewHolder.PasswordEdit)
                .For(v => v.TextAndTextChangedBinding())
                .To(vm => vm.Password);

            bindingSet
                .Bind(ViewHolder.SignInButton)
                .For(v => v.ClickBinding())
                .To(vm => vm.LoginCmd);

            bindingSet
                .Bind(ViewHolder.SignInButton)
                .For(v => v.EnabledBinding())
                .To(vm => vm.LoginCmd);

            bindingSet
                .Bind(ViewHolder.ErrorOverlayView)
                .For(v => v.VisibilityBinding())
                .To(vm => vm.ShowError)
                .WithConversion<VisibleGoneValueConverter>();

            bindingSet
                .Bind(ViewHolder.ErrorOverlayTriangleImage)
                .For(v => v.VisibilityBinding())
                .To(vm => vm.ShowError)
                .WithConversion<VisibleGoneValueConverter>();

            bindingSet
                .Bind(ViewHolder.ErrorText)
                .For(v => v.TextBinding())
                .To(vm => vm.ErrorMessage);
        }
    }
}
