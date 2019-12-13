using System;
using System.Threading;
using Acr.UserDialogs;
using Android.App;
using Android.OS;
using Android.Runtime;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;
using XMP.API.Bootstrappers;
using XMP.Core.Bootstrapper;
using XMP.Droid.Bootstrapper;

namespace XMP.Droid
{
    [Application]
    public class Application : Android.App.Application, Android.App.Application.IActivityLifecycleCallbacks
    {
        protected Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public static Activity CurrentActivity { get; private set; }

        public override void OnCreate()
        {
            base.OnCreate();

            InitFramework();

            RegisterActivityLifecycleCallbacks(this);

            UserDialogs.Init(() => CurrentActivity);

            DatePromptConfig.DefaultAndroidStyleId = Resource.Style.DialogTheme;

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CurrentActivity = activity;

            Xamarin.Essentials.Platform.Init(activity, savedInstanceState);
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CurrentActivity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CurrentActivity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }

        private void InitFramework()
        {
            var config = new BootstrapperConfig();

            config.SetSimpleIoc(new SimpleIoc());

            var compositeBootstrapper = new CompositeBootstrapper(new AndroidBootstrapper(), new CoreBootstrapper());

            compositeBootstrapper.Execute(config);
        }
    }
}
