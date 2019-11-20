﻿using System;
using FlexiMvvm.Bootstrappers;
using XMP.Core.Bootstrapper;
using XMP.Core.Navigation;
using XMP.iOS.Navigation;

namespace XMP.iOS.Bootstrapper
{
    internal sealed class IosBootstrapper : IBootstrapper
    {
        public void Execute(BootstrapperConfig config)
        {
            var simpleIoc = config.GetSimpleIoc();

            simpleIoc.Register<INavigationService>(() => new AppNavigationService());
        }
    }
}
