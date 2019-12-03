using System;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
using XMP.Core.ViewModels.Launcher;
using XMP.Core.ViewModels.Login;
using XMP.Core.ViewModels.Main;
using XMP.Core.ViewModels.Details;
using Acr.UserDialogs;
using XMP.API.Bootstrappers;
using XMP.API.Services.Abstract;
using XMP.Core.Services.Abstract;
using XMP.Core.Services.Implementation;
using FlexiMvvm.Operations;
using XMP.Core.Operations;

namespace XMP.Core.Bootstrapper
{
    public sealed class CoreBootstrapper : IBootstrapper
    {
        public void Execute(BootstrapperConfig config)
        {
            InitApi(config);

            var simpleIoc = config.GetSimpleIoc();

            simpleIoc.Register<IOperationFactory>(() => new OperationFactory(simpleIoc, new OperationErrorHandler()), FlexiMvvm.Ioc.Reuse.Singleton);

            simpleIoc.Register<IUserDialogs>(() => UserDialogs.Instance, FlexiMvvm.Ioc.Reuse.Singleton);

            simpleIoc.Register(() => new LauncherViewModel(simpleIoc.Get<INavigationService>()));

            simpleIoc.Register(() => new LoginViewModel(simpleIoc.Get<INavigationService>(), simpleIoc.Get<IUserDialogs>(), simpleIoc.Get<ISessionService>(), simpleIoc.Get<IOperationFactory>()));

            simpleIoc.Register(() => new MainViewModel(simpleIoc.Get<INavigationService>()));

            simpleIoc.Register(() => new DetailsViewModel(simpleIoc.Get<INavigationService>(), simpleIoc.Get<IUserDialogs>()));

            LifecycleViewModelProvider.SetFactory(new DefaultLifecycleViewModelFactory(simpleIoc));

            simpleIoc.Register<ISessionService>(() => new SessionService(simpleIoc.Get<IApiSettingsService>(), simpleIoc.Get<IAuthenticationApiService>()), FlexiMvvm.Ioc.Reuse.Singleton);

            simpleIoc.Get<IApiSettingsService>().RefreshTokenProvider = simpleIoc.Get<ISessionService>();
        }

        private static void InitApi(BootstrapperConfig config)
        {
            var apiBootstrapper = new ApiBootstrapper();

            apiBootstrapper.Execute(config);
        }
    }
}
