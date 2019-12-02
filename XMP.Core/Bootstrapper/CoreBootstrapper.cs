using System;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
using XMP.Core.ViewModels.Launcher;
using XMP.Core.ViewModels.Login;
using XMP.Core.ViewModels.Main;
using XMP.Core.ViewModels.Details;
using Acr.UserDialogs;

namespace XMP.Core.Bootstrapper
{
    public sealed class CoreBootstrapper : IBootstrapper
    {
        public void Execute(BootstrapperConfig config)
        {
            var simpleIoc = config.GetSimpleIoc();

            simpleIoc.Register<IUserDialogs>(() => UserDialogs.Instance);

            simpleIoc.Register(() => new LauncherViewModel(simpleIoc.Get<INavigationService>()));

            simpleIoc.Register(() => new LoginViewModel(simpleIoc.Get<INavigationService>()));

            simpleIoc.Register(() => new MainViewModel(simpleIoc.Get<INavigationService>()));

            simpleIoc.Register(() => new DetailsViewModel(simpleIoc.Get<INavigationService>(), simpleIoc.Get<IUserDialogs>()));

            LifecycleViewModelProvider.SetFactory(new DefaultLifecycleViewModelFactory(simpleIoc));
        }
    }
}
