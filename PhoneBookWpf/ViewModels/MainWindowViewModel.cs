using PhoneBookWpf.Commands;
using PhoneBookWpf.Models;
using PhoneBookWpf.StaticComponents;
using PhoneBookWpf.ViewModels.Base;
using PhoneBookWpf.Views.Windows;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows.Input;

namespace PhoneBookWpf.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Свойства и поля
        /// <summary>
        /// Коллекция записей
        /// </summary>
      
        private IEnumerable<Contact> _contacts;
      
        public IEnumerable<Contact> Contacts
        {
            get { return _contacts; }
            set
            {

                Set(ref _contacts, value);
            }
        }
       
        private async Task InitializePersones()
        {
            Contacts = await User._apiRequests.GetContacts();
        }
      
        /// <summary>
        /// Выделенный эл-т в listView
        /// </summary>
        private Contact _selectedItem;
      
        public Contact SelectedItem
        {
            get => _selectedItem;
            set => Set(ref _selectedItem, value);
        }
      
        #endregion
      
        #region Команды
      
        #region Добавление Contact
       
        public ICommand AddContactCommand { get; }
       
        private bool CanAddContactExecute(object parameter) => true;
      
        private void OnAddContactExecuted(object parameter)
        {
            AddWindow add = new();
            add.Show();
        }
      
        #endregion       
      
        # region Logout
       
        public ICommand LogoutCommand { get; }
       
        private bool CanLogoutExecute(object parameter) => true;
       
        private void OnlogoutExecuted(object parameter)
        {
            User._tokenResponse = null;
            User._userModel = null;
            MainWindow? mainWindow = parameter as MainWindow;
            AuthorizeWindow authorizeWindow = new();
            authorizeWindow.Show();
            mainWindow.Close();
        }
       
        #endregion
       
        #region Users
        
        public ICommand UsersCommand { get; }
        
        private bool CanUsersExecute(object parameter) => true;
      
        private void OnUsersExecuted(object parameter)
        {
            UsersWindow usersWindow = new();
            usersWindow.Show();
        }
      
        #endregion
      
        #region Изменение Contact
      
        public ICommand EditCommand { get; }
      
        private bool CanEditCommandExecute(object parameter) => true;
      
        private void OnEditCommandExecuted(object parameter)
        {
            if (_selectedItem == null) { return; }
            EditWindow editWindow = new(_selectedItem);
            editWindow.Show();
        }
      
        #endregion
      
        #region Удаление Contact
      
        public ICommand DeleteCommand { get; }
     
        private bool CanDeleteCommandExecute(object parameter) => true;
      
        private async void OnDeleteCommandExecuted(object parameter)
        {
            try
            {
                if (_selectedItem == null) { return; }
                await User._apiRequests.ContactDelete(_selectedItem.ID, User._tokenResponse.token);
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
      
        #region Обновление Contacts
      
        public ICommand InitContactsCommand { get; }
       
        private bool CanInitContactsCommandExecute(object parameter) => true;
      
        private void OnInitContactsCommandExecuted(object parameter)
        {
            InitializePersones();
        }
        #endregion
        #endregion
       
        public MainWindowViewModel()
        {
            InitializePersones();
            #region Команды
            AddContactCommand = new lambdaCommand(OnAddContactExecuted, CanAddContactExecute);
            LogoutCommand = new lambdaCommand(OnlogoutExecuted, CanLogoutExecute);
            UsersCommand = new lambdaCommand(OnUsersExecuted, CanUsersExecute);
            EditCommand = new lambdaCommand(OnEditCommandExecuted, CanEditCommandExecute);
            DeleteCommand = new lambdaCommand(OnDeleteCommandExecuted, CanDeleteCommandExecute);
            InitContactsCommand = new lambdaCommand(OnInitContactsCommandExecuted, CanInitContactsCommandExecute);
            #endregion
        }

    }
}
