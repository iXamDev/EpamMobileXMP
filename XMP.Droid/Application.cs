using System;
using Android.App;
using Android.Runtime;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;
using XMP.API.Bootstrappers;
using XMP.Core.Bootstrapper;
using XMP.Droid.Bootstrapper;

namespace XMP.Droid
{
    [Application]
    public class Application : Android.App.Application
    {
        protected Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            InitFramework();
        }

        private void InitFramework()
        {
            var config = new BootstrapperConfig();

            config.SetSimpleIoc(new SimpleIoc());

            var compositeBootstrapper = new CompositeBootstrapper(new AndroidBootstrapper(this), new CoreBootstrapper());

            compositeBootstrapper.Execute(config);
        }
    }
}
