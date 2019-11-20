using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using FlexiMvvm.Views;
using XMP.Core.ViewModels.Launcher;

namespace XMP.Droid.Views.Splash
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.XMP.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : BindableAppCompatActivity<LauncherViewModel>
    {

    }
}

