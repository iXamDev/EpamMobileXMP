using System;
using Android.App;
using Android.OS;
using FlexiMvvm.Bindings;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Login;
using Android.Widget;
using FlexiMvvm.ValueConverters;

namespace XMP.Droid.Views.Login
{
    [Activity]
    public class LoginActivity : BindableAppCompatActivity<LoginViewModel>
    {
        private Button loginButton;

        private LinearLayout errorOverlay;

        private ImageView errorTriangle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);

            loginButton = FindViewById<Button>(Resource.Id.sign_in_button);

            errorOverlay = FindViewById<LinearLayout>(Resource.Id.error_overlay_view);

            errorTriangle = FindViewById<ImageView>(Resource.Id.error_overlay_triangle_image);
        }

        public override void Bind(BindingSet<LoginViewModel> bindingSet)
        {
            base.Bind(bindingSet);

            bindingSet
                .Bind(loginButton)
                .For(v => v.ClickBinding())
                .To(vm => vm.LoginCmd);

            bindingSet
                .Bind(errorOverlay)
                .For(v => v.VisibilityBinding())
                .To(v => v.ShowError)
                .WithConversion<VisibleGoneValueConverter>();

            bindingSet
                .Bind(errorTriangle)
                .For(v => v.VisibilityBinding())
                .To(v => v.ShowError)
                .WithConversion<VisibleGoneValueConverter>();
        }
    }
}
