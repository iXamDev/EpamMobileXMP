using System;
using FlexiMvvm.Bootstrappers;
using XMP.API.Services.Abstract;
using XMP.API.Services.Implementation;
using System.Net.Http;

namespace XMP.API.Bootstrappers
{
    public class ApiBootstrapper : IBootstrapper
    {
        public void Execute(BootstrapperConfig config)
        {
            var simpleIoc = config.GetSimpleIoc();

            simpleIoc.Register<IWebConnectionService>(() => new WebConnectionService(), FlexiMvvm.Ioc.Reuse.Singleton);

            simpleIoc.Register<IApiSettingsService>(() => new ApiSettingsService(simpleIoc.Get<IWebConnectionService>()), FlexiMvvm.Ioc.Reuse.Singleton);

            simpleIoc.Register<IAuthenticationApiService>(() => new AuthenticationApiService(simpleIoc.Get<IWebConnectionService>(), simpleIoc.Get<IApiSettingsService>()), FlexiMvvm.Ioc.Reuse.Singleton);

            simpleIoc.Register<IVacationRequestsApiService>(() => new VacationRequestsApiService(simpleIoc.Get<IWebConnectionService>(), simpleIoc.Get<IApiSettingsService>()), FlexiMvvm.Ioc.Reuse.Singleton);
        }
    }
}
