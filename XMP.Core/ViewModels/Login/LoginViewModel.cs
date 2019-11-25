using System;
using FlexiMvvm.ViewModels;
using System.Windows.Input;
using System.Threading.Tasks;
using XMP.Core.Navigation;

namespace XMP.Core.ViewModels.Login
{
    public class LoginViewModel : LifecycleViewModel
    {
        #region Commands

        public ICommand LoginCmd => CommandProvider.GetForAsync(OnLogin);

        #endregion

        #region Properties

        private string login;
        public string Login
        {
            get => login;
            set => SetValue(ref login, value, nameof(Login));
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetValue(ref password, value, nameof(Password));
        }

        private bool showError;
        public bool ShowError
        {
            get => showError;
            private set => SetValue(ref showError, value, nameof(ShowError));
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get => errorMessage;
            private set => SetValue(ref errorMessage, value, nameof(ErrorMessage));
        }

        public string LoginButtonTitle => "Sign in".ToUpper();

        public string LoginHint => "Login";

        public string PasswordHint => "Password";

        #endregion

        #region Services

        protected INavigationService NavigationService { get; }

        #endregion

        #region Constructor

        public LoginViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        #endregion

        #region Private

        private Task OnLogin()
        {
            NavigationService.NavigateToMain(this);

            return Task.FromResult(0);
        }

        #endregion

        #region Protected

        #endregion

        #region Public

        #endregion

    }
}
