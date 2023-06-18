using PhoneBookWpf.Commands;
using PhoneBookWpf.Models;
using PhoneBookWpf.StaticComponents;
using PhoneBookWpf.ViewModels.Base;
using PhoneBookWpf.Views.Windows;
using System.Net;
using System.Web.Http;
using System.Windows.Input;

namespace PhoneBookWpf.ViewModels
{
    internal class AuthorizeWindowViewModel : ViewModel
    {
        #region Поля и свойства
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                Set(ref _login, value);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                Set(ref _password, value);
            }
        }
        #endregion
        #region Команды
        #region Login
        public ICommand LoginCommand { get; }
        private bool CanLoginCommandExecute(object parameter) => true;
        private async void OnLoginCommandExecuted(object parameter)
        {
            try
            {
                if (Login == string.Empty || Password == string.Empty) { ErrorContent = "Заполните все поля."; return; }
                UserModel userModel = new() { Username = Login, Password = Password };
                TokenResponse token = await User._apiRequests.LoginRequest(userModel);
                User._tokenResponse = token;
                User._userModel = userModel;
                AuthorizeWindow? authorizeWindow = parameter as AuthorizeWindow;
                MainWindow main = new();
                main.Show();
                authorizeWindow?.Close();
            }
            catch (HttpResponseException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ErrorContent = "Пользователь не найден.";
                }
                else
                {
                    ErrorContent = ex.Message;
                }
            }
        }
        #endregion
        #region Register
        public ICommand RegisterCommand { get; }
        private bool CanRegisterCommandExecute(object parameter) => true;
        private async void OnRegisterCommandExecuted(object parameter)
        {
            try
            {
                if (Login == string.Empty || Password == string.Empty) { ErrorContent = "Заполните все поля."; return; }
                UserModel userModel = new() { Username = Login, Password = Password };
                TokenResponse token = await User._apiRequests.RegisterRequest(userModel);
                User._tokenResponse = token;
                User._userModel = userModel;
                AuthorizeWindow? authorizeWindow = parameter as AuthorizeWindow;
                MainWindow main = new();
                main.Show();
                authorizeWindow?.Close();
            }
            catch (HttpResponseException ex)
            {

                if (ex.Response.StatusCode == HttpStatusCode.Conflict)
                {
                    ErrorContent = "Пользователь с таким логином уже существует.";
                }
                else
                {
                    ErrorContent = ex.Message;
                }
            }
        }
        #endregion
        #region Пропуск входа
        public ICommand SkipCommand { get; }
        private bool CanSkipCommandExecute(object parameter) => true;
        private void OnSkipCommandExecuted(object parameter)
        {
            AuthorizeWindow? authorizeWindow = parameter as AuthorizeWindow;
            DemoViewWindow main = new();
            main.Show();
            authorizeWindow?.Close();
        }
        #endregion
        #endregion
        public AuthorizeWindowViewModel()
        {
            LoginCommand = new lambdaCommand(OnLoginCommandExecuted, CanLoginCommandExecute);
            RegisterCommand = new lambdaCommand(OnRegisterCommandExecuted, CanRegisterCommandExecute);
            SkipCommand = new lambdaCommand(OnSkipCommandExecuted, CanSkipCommandExecute);
        }
    }
}
