using System;
using System.Windows.Input;
using FlexiMvvm.ViewModels;
using XMP.Core.Navigation;

namespace XMP.Core.ViewModels.Details
{
    public class DetailsViewModel : LifecycleViewModel
    {
        public ICommand SaveCmd => CommandProvider.Get(OnSave);

        public string ScreenTitle => "Request";

        private void OnSave()
        {
            NavigationService.NavigateBack(this);
        }

        protected INavigationService NavigationService { get; }

        public DetailsViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}
