using PhoneBookWpf.Commands;
using PhoneBookWpf.Models;
using PhoneBookWpf.StaticComponents;
using PhoneBookWpf.ViewModels.Base;
using PhoneBookWpf.Views.Windows;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PhoneBookWpf.ViewModels
{
    internal class DemoViewWindowModel : ViewModel
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
        #region Login
      
        public ICommand LoginCommand { get; }
      
        private bool CanLoginCommandExecute(object parameter) => true;
      
        private void OnLoginCommandExecuted(object parameter)
        {
            DemoViewWindow demoViewWindow = (DemoViewWindow)parameter;
            AuthorizeWindow authorize = new();
            authorize.Show();
            demoViewWindow.Close();
        }
     
        #endregion
        #endregion

        public DemoViewWindowModel()
        {
            InitializePersones();
            LoginCommand = new lambdaCommand(OnLoginCommandExecuted, CanLoginCommandExecute);
        }
    }
}
