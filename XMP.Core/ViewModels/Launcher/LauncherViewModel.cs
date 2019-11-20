using System;
using System.Threading.Tasks;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;
namespace XMP.Core.ViewModels.Launcher
{
    public class LauncherViewModel : LifecycleViewModel
    {
        protected INavigationService NavigationService { get; }

        public LauncherViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public override Task InitializeAsync(bool recreated)
        {
            return base.InitializeAsync(recreated);
        }
    }
}
