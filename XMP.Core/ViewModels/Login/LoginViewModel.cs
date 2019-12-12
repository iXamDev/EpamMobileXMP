using System;
using System.Diagnostics;
using System.Security.Authentication;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FlexiMvvm.Commands;
using FlexiMvvm.Operations;
using FlexiMvvm.ViewModels;
using XMP.Core.Models;
using XMP.Core.Navigation;
using XMP.Core.Operations;
using XMP.Core.Services.Abstract;

namespace XMP.Core.ViewModels.Login
{
    public class LoginViewModel : LifecycleViewModel
    {
        private IOperationHandlerBuilder<bool> _loginOperation;

        private string _password;

        private string _login;

        private bool _showError;

        private string _errorMessage;

        public LoginViewModel(INavigationService navigationService, IUserDialogs userDialogs, ISessionService sessionService, IOperationFactory operationFactory)
        {
            NavigationService = navigationService;

            UserDialogs = userDialogs;

            SessionService = sessionService;

            OperationFactory = operationFactory;

            _loginOperation =
                OperationFactory
                    .Create(this)
                    .WithLoadingNotification()

                    // .WithPreventRepetitiveExecutions()
                    .WithExpressionAsync((cancellationToken) =>
                    {
                        if (ValidateCredentials(out var credentials))
                        {
                            return SessionService.Start(credentials);
                        }

                        return Task.FromResult(false);
                    })
                    .OnSuccess(success =>
                    {
                        if (success)
                            NavigationService.NavigateToMain(this);
                    })
                    .OnError<InvalidCredentialException>(ex => SetErrorMessage(LoginErrorCases.WrongCredentials))
                    .OnError<Exception>(ex => SetErrorMessage(LoginErrorCases.GeneralError));

            SetDebugCredentials();
        }

        public Command LoginCmd => CommandProvider.GetForAsync(OnLogin);

        public string Login
        {
            get => _login;
            set
            {
                SetValue(ref _login, value, nameof(Login));

                HideErrorMessage();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetValue(ref _password, value, nameof(Password));
            }
        }

        public bool ShowError
        {
            get => _showError;
            private set => SetValue(ref _showError, value, nameof(ShowError));
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetValue(ref _errorMessage, value, nameof(ErrorMessage));
        }

        public string LoginButtonTitle => "Sign in".ToUpper();

        public string LoginHint => "Login";

        public string PasswordHint => "Password";

        protected INavigationService NavigationService { get; }

        protected IUserDialogs UserDialogs { get; }

        protected ISessionService SessionService { get; }

        protected IOperationFactory OperationFactory { get; }

        [Conditional("DEBUG")]
        private void SetDebugCredentials()
        {
            Login = "ark";
            Password = "123";
        }

        private Task OnLogin()
        => _loginOperation.ExecuteAsync();

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
    }
}
