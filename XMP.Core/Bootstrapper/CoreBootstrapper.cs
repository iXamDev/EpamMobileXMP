using System;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
using XMP.Core.ViewModels.Launcher;
using XMP.Core.ViewModels.Login;

namespace XMP.Core.Bootstrapper
{
    public sealed class CoreBootstrapper : IBootstrapper
    {
        public void Execute(BootstrapperConfig config)
        {
            var simpleIoc = config.GetSimpleIoc();

            simpleIoc.Register(() => new LauncherViewModel(simpleIoc.Get<INavigationService>()));

            simpleIoc.Register(() => new LoginViewModel(simpleIoc.Get<INavigationService>()));

            LifecycleViewModelProvider.SetFactory(new DefaultLifecycleViewModelFactory(simpleIoc));
        }
    }
}
