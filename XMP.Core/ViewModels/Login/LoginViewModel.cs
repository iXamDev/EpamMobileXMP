using System;
using FlexiMvvm.ViewModels;
using System.Windows.Input;
using System.Threading.Tasks;
using XMP.Core.Navigation;
using XMP.API.Services.Abstract;
using XMP.Core.Services.Abstract;
using System.Security.Authentication;
using Acr.UserDialogs;
using XMP.Core.Models;
using FlexiMvvm.Commands;
using FlexiMvvm.Operations;
using XMP.Core.Operations;

namespace XMP.Core.ViewModels.Login
{
    public class LoginViewModel : LifecycleViewModel
    {
        #region Fields

        private IOperationHandlerBuilder<bool> loginOperation;

        #endregion

        #region Commands

        public Command LoginCmd => CommandProvider.GetForAsync(OnLogin);

        #endregion

        #region Properties

        private string login;
        public string Login
        {
            get => login;
            set
            {
                SetValue(ref login, value, nameof(Login));

                HideErrorMessage();
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                SetValue(ref password, value, nameof(Password));
            }
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

        protected IUserDialogs UserDialogs { get; }

        protected ISessionService SessionService { get; }

        protected IOperationFactory OperationFactory { get; }

        #endregion

        #region Constructor

        public LoginViewModel(INavigationService navigationService, IUserDialogs userDialogs, ISessionService sessionService, IOperationFactory operationFactory)
        {
            NavigationService = navigationService;

            UserDialogs = userDialogs;

            SessionService = sessionService;

            OperationFactory = operationFactory;

            loginOperation =
                OperationFactory
                    .Create(this)
                    .WithLoadingNotification()
                    //.WithPreventRepetitiveExecutions()
                    .WithExpressionAsync((cancellationToken) =>
                    {
                        if (ValidateCredentials(out var credentials))
                        {
                            return SessionService.Start(credentials);
                        }

                        return Task.FromResult(false);
                    })
                    .OnSuccess(success => { if (success) NavigationService.NavigateToMain(this); })
                    .OnError<InvalidCredentialException>(ex => SetErrorMessage(LoginErrorCases.WrongCredentials))
                    .OnError<Exception>(ex => SetErrorMessage(LoginErrorCases.GeneralError));

#if DEBUG
            Login = "ark";
            Password = "123";
#endif
        }

        #endregion

        #region Private

        private Task OnLogin()
        => loginOperation.ExecuteAsync();

        private bool ValidateCredentials(out UserCredentials credentials)
        {
            credentials = null;

            if (string.IsNullOrEmpty(Login))
            {
                SetErrorMessage(LoginErrorCases.EmptyLogin);
                return false;
            }

            if (string.IsNullOrEmpty(Password))
            {
                SetErrorMessage(LoginErrorCases.EmptyPassword);
                return false;
            }

            credentials = new UserCredentials
            {
                Login = Login,
                Password = Password
            };

            return true;
        }

        private void SetErrorMessage(LoginErrorCases errorCase)
        {
            switch (errorCase)
            {
                case LoginErrorCases.EmptyLogin:
                    ErrorMessage = "Please, enter your login";
                    break;

                case LoginErrorCases.EmptyPassword:
                    ErrorMessage = "Please, enter your and password";
                    break;

                case LoginErrorCases.WrongCredentials:
                    ErrorMessage = "Please, retry your login and password pair. Check current Caps Lock and input language settings";
                    break;

                default:
                    ErrorMessage = "An error has occure, please retry later";
                    break;
            }

            ShowError = true;
        }

        private void HideErrorMessage()
        {
            ShowError = false;

            ErrorMessage = null;
        }

        #endregion
    }
}
