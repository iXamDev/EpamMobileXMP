using FlexiMvvm.Bootstrappers;
using FlexiMvvm.Ioc;
using NN.Ios.Core.Views;
using NN.Ios.Core.Window;
using NN.Ios.FlexiMvvm.Presenter;
using NN.Ios.FlexiMvvm.ViewCache;
using NN.Ios.FlexiMvvm.Views;
using NN.Shared.FlexiMvvm.Presenters;
using NN.Shared.FlexiMvvm.Views;
using XMP.API.Bootstrappers;

namespace XMP.IOS.Bootstrapper
{
    internal sealed class IosBootstrapper : IBootstrapper
    {
        public void Execute(BootstrapperConfig config)
        {
            var simpleIoc = config.GetSimpleIoc();

            var viewResolver = new IosReflectionViewResolver(new[] { GetType().Assembly });

            simpleIoc.Register<IViewResolver>(() => viewResolver, Reuse.Singleton);

            var windowHolder = new WindowHolderWrapper(() => AppDelegate.Window, w => AppDelegate.Window = w);

            var viewPresenter = new FlexiMvvmIosViewPresenter(windowHolder, new ActivatorViewFactory(), new LifecycleViewModelToViewControllerCache());

            simpleIoc.Register<IFlexiMvvmViewPresenter>(() => viewPresenter, Reuse.Singleton);
        }
    }
}
