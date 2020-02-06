using Acr.UserDialogs;
using ExpressMapper;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;
using FlexiMvvm.Operations;
using FlexiMvvm.ViewModels;
using NN.Shared.FlexiMvvm.Extensions;
using NN.Shared.FlexiMvvm.Navigation;
using NN.Shared.FlexiMvvm.Presenters;
using NN.Shared.FlexiMvvm.Views;
using XMP.API.Bootstrappers;
using XMP.API.Services.Abstract;
using XMP.Core.Database.Abstract;
using XMP.Core.Database.Implementation.RealmDatabase;
using XMP.Core.Database.Implementation.RealmDatabase.VacationRequests;
using XMP.Core.Mapping;
using XMP.Core.Models;
using XMP.Core.Operations;
using XMP.Core.Services.Abstract;
using XMP.Core.Services.Implementation;
using XMP.Core.ViewModels.Login;

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

            simpleIoc.Register<INavigationService>(
                () => new NavigationService(simpleIoc.Get<IFlexiMvvmViewPresenter>(), simpleIoc.Get<IViewResolver>()),
                Reuse.Singleton);

            IocHelper.RegisterViewModelsFromAssemblyByReflection(simpleIoc, GetType().Assembly);

            LifecycleViewModelProvider.SetFactory(new DefaultLifecycleViewModelFactory(simpleIoc));

            simpleIoc.Register<ISessionService>(() => new SessionService(simpleIoc.Get<IApiSettingsService>(), simpleIoc.Get<IAuthenticationApiService>()), FlexiMvvm.Ioc.Reuse.Singleton);

            var session = simpleIoc.Get<ISessionService>();

            simpleIoc.Get<IApiSettingsService>().RefreshTokenUpdater = session;

            session.OnCredentialsFails += async (sender, e) =>
            {
                await simpleIoc.Get<INavigationService>().Navigate<LoginViewModel>();

                await simpleIoc.Get<IRealmProvider>().Drop().ConfigureAwait(false);
            };

            RegisterMappings();
        }

        private void RegisterMappings()
        {
            VacationMappings.RegisterMappings();

            Mapper.Compile();
        }

        private void InitApi(BootstrapperConfig config)
        {
            var apiBootstrapper = new ApiBootstrapper();

            apiBootstrapper.Execute(config);
        }
    }
}
