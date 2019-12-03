using System;
using FlexiMvvm.Bootstrappers;
using XMP.API.Bootstrappers;
using XMP.Core.Navigation;
using XMP.Droid.Navigation;

namespace XMP.Droid.Bootstrapper
{
    internal sealed class AndroidBootstrapper : IBootstrapper
    {
        public void Execute(BootstrapperConfig config)
        {
            var simpleIoc = config.GetSimpleIoc();
            simpleIoc.Register<INavigationService>(() => new AppNavigationService());
        }
    }
}
