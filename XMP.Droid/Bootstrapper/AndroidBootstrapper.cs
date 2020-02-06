using System.Threading;
using Acr.UserDialogs;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;
using NN.Droid.Core.Coordinators.Activities;
using NN.Droid.Core.Lifecycle;
using NN.Droid.FlexiMvvm.Presenter;
using NN.Droid.FlexiMvvm.Views;
using NN.Droid.FlexiMvvm.Views.ViewCache;
using NN.Shared.FlexiMvvm.Presenters;
using NN.Shared.FlexiMvvm.Views;
using XMP.API.Bootstrappers;

namespace XMP.Droid.Bootstrapper
{
    internal sealed class AndroidBootstrapper : IBootstrapper
    {
        private Application _application;

        public AndroidBootstrapper(Application application)
        {
            _application = application;

            Xamarin.Essentials.Platform.Init(application);

            DatePromptConfig.DefaultAndroidStyleId = Resource.Style.DialogTheme;

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
        }

        public void Execute(BootstrapperConfig config)
        {
            var simpleIoc = config.GetSimpleIoc();

            var lifecycleMonitor = new ActivityLifecycleMonitor(_application);

            UserDialogs.Init(() => lifecycleMonitor.CurrentActivity);

            var viewResolver = new AndroidReflectionViewResolver(new[] { GetType().Assembly });

            simpleIoc.Register<IViewResolver>(() => viewResolver, Reuse.Singleton);

            var activityCoordinator = new ActivityCoordinator(lifecycleMonitor);

            var viewPresenter = new FlexiMvvmAndroidViewPresenter(
                activityCoordinator,
                null,
                null,
                null);

            simpleIoc.Register<IFlexiMvvmViewPresenter>(() => viewPresenter, Reuse.Singleton);

            UserDialogs.Init(_application);
        }
    }
}
