﻿using System;
using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;

namespace XMP.API.Bootstrappers
{
    public static class BootstrapperConfigExtensions
    {
        private const string SimpleIocKey = "SimpleIoc";

        public static ISimpleIoc GetSimpleIoc(this BootstrapperConfig config)
        {
            return config.GetValue<ISimpleIoc>(SimpleIocKey);
        }

        public static void SetSimpleIoc(this BootstrapperConfig config, ISimpleIoc simpleIoc)
        {
            config.SetValue(SimpleIocKey, simpleIoc);
        }
    }
}
