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
using ExpressMapper;
using XMP.API.Models;
using XMP.Core.Models;
using XMP.Core.Database.Abstract;
using XMP.Core.Database.Implementation.RealmDatabase;
using XMP.Core.Mapping;
using XMP.Core.Database.Implementation.RealmDatabase.VacationRequests;
using Xamarin.Essentials;

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

            simpleIoc.Register<IRealmProvider>(() => new RealmProvider(), FlexiMvvm.Ioc.Reuse.Singleton);

            simpleIoc.Register<IVacationRequestsRepository>(() => new VacationRequestsRepository(new ExpressMapperRealmRepositoryEntriesMapper<VacantionRequest, VacationRequestRealmObject>(), simpleIoc.Get<IRealmProvider>()));

            simpleIoc.Register<IVacationRequestsManagerService>(() => new VacationRequestsManagerService(simpleIoc.Get<IVacationRequestsRepository>(), simpleIoc.Get<IVacationRequestsApiService>()), FlexiMvvm.Ioc.Reuse.Singleton);

            simpleIoc.Register<IVacationRequestsFilterService>(() => new VacationRequestsFilterService());

            simpleIoc.Register(() => new LauncherViewModel(simpleIoc.Get<INavigationService>(), simpleIoc.Get<ISessionService>()));

            simpleIoc.Register(() => new LoginViewModel(simpleIoc.Get<INavigationService>(), simpleIoc.Get<IUserDialogs>(), simpleIoc.Get<ISessionService>(), simpleIoc.Get<IOperationFactory>()));

            simpleIoc.Register(() => new MainViewModel(simpleIoc.Get<IVacationRequestsManagerService>(), simpleIoc.Get<IVacationRequestsFilterService>(), simpleIoc.Get<ISessionService>(), simpleIoc.Get<IUserDialogs>(), simpleIoc.Get<INavigationService>()));

            simpleIoc.Register(() => new DetailsViewModel(simpleIoc.Get<ISessionService>(), simpleIoc.Get<IVacationRequestsManagerService>(), simpleIoc.Get<INavigationService>(), simpleIoc.Get<IUserDialogs>()));

            LifecycleViewModelProvider.SetFactory(new DefaultLifecycleViewModelFactory(simpleIoc));

            simpleIoc.Register<ISessionService>(() => new SessionService(simpleIoc.Get<IApiSettingsService>(), simpleIoc.Get<IAuthenticationApiService>()), FlexiMvvm.Ioc.Reuse.Singleton);

            var session = simpleIoc.Get<ISessionService>();

            simpleIoc.Get<IApiSettingsService>().RefreshTokenUpdater = session;

            session.OnCredentialsFails += async (sender, e) =>
            {
                MainThread.BeginInvokeOnMainThread(simpleIoc.Get<INavigationService>().NavigateToLogin);

                await simpleIoc.Get<IRealmProvider>().Drop().ConfigureAwait(false);
            };

            RegisterMappings();
        }

        private void RegisterMappings()
        {
            VacationMappings.RegisterMappings();

            Mapper.Compile();
        }

        private static void InitApi(BootstrapperConfig config)
        {
            var apiBootstrapper = new ApiBootstrapper();

            apiBootstrapper.Execute(config);
        }
    }
}
