using Android.App;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Launcher;

namespace XMP.Droid.Views.Splash
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.XMP.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity
        : BindableAppCompatActivity<LauncherViewModel>
    {
    }
}
