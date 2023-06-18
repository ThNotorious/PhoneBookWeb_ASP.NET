using PhoneBookWpf.Commands;
using PhoneBookWpf.Models;
using PhoneBookWpf.StaticComponents;
using PhoneBookWpf.ViewModels.Base;
using PhoneBookWpf.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows.Input;

namespace PhoneBookWpf.ViewModels
{
    internal class UsersWindowViewModel : ViewModel
    {
        #region Поля и свойства
        #region Коллекция Users
        private IEnumerable<UserModel> _users;
        public IEnumerable<UserModel> Users
        {
            get { return _users; }
            set { Set(ref _users, value); }
        }
        #endregion
       
        #region Выбранный User
        private UserModel _selectedUser;
        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set { Set(ref _selectedUser, value); }
        }
        #endregion
      
        /// <summary>
        /// Получение всех Users
        /// </summary>
        /// <returns></returns>
        private async Task GetUsers()
        {
            try
            {
                Users = await User._apiRequests.GetUsers(User._tokenResponse.token);
            }
            catch (HttpResponseException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.Unauthorized) { ErrorContent = "Авторизуйтесь."; }
                else if (ex.Response.StatusCode == HttpStatusCode.Forbidden) { ErrorContent = "К сожалению, у вас нет доступа."; }
                else
                {
                    ErrorContent = ex.Message;
                }
            }
        }
        #endregion
       
        #region Команды
      
        #region Удаление User
        public ICommand DeleteCommand { get; }
        private bool CanDeleteCommandExecute(object parameter) => true;
        private async void OnDeleteCommandExecuted(object parameter)
        {
            if (SelectedUser == null) { return; }
            try
            {
                await User._apiRequests.DeleteUser(SelectedUser.Username, User._tokenResponse.token);
            }
            catch (HttpResponseException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.Unauthorized) { ErrorContent = "Авторизуйтесь."; }
                else if (ex.Response.StatusCode == HttpStatusCode.Forbidden) { ErrorContent = "К сожалению, у вас нет доступа."; }
                else if (ex.Response.StatusCode == HttpStatusCode.NotFound) { ErrorContent = "Пользователь не найден."; }
                else
                {
                    ErrorContent = ex.Message;
                }
            }
        }
      
        #endregion
     
        #region Close
       
        public ICommand CloseCommand { get; }
        private bool CanCloseCommandExecute(object parameter) => true;
        private void OnCloseCommandExecuted(object parameter)
        {
            UsersWindow usersWindow = (UsersWindow)parameter;
            usersWindow.Close();
        }
     
        #endregion
        #endregion
      
        public UsersWindowViewModel()
        {
            GetUsers();
            DeleteCommand = new lambdaCommand(OnDeleteCommandExecuted, CanDeleteCommandExecute);
            CloseCommand = new lambdaCommand(OnCloseCommandExecuted, CanCloseCommandExecute);
        }
    }
}
