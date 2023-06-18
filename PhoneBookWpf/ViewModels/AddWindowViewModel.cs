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
    internal class AddWindowViewModel : ViewModel
    {
        #region Поля и свойства
      
        private Contact _contact;
       
        public Contact Contact
        {
            get { return _contact; }
            set { Set(ref _contact, value); }
        }
     
        #endregion
      
        #region Команды
        #region Добавление Persone
      
        public ICommand AddCommand { get; }
      
        private bool CanAddCommandExecute(object parameter) => true;
     
        private async void OnAddCommandExecuted(object parameter)
        {
            if (Contact.SecondName == null || Contact.FirstName == null || Contact.MiddleName == null ||
            Contact.PhoneNumber == null || Contact.Description == null)
            {
                ErrorContent = "Заполните все поля"; return;
            }
            try
            {
                await User._apiRequests.ContactAdd(Contact, User._tokenResponse.token);
                AddWindow addWindow = (AddWindow)parameter;
                addWindow.Close();
            }
            catch (HttpResponseException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ErrorContent = "К сожалению, у вас нет доступа.";
                }
                else
                {
                    ErrorContent = ex.Message;
                }
            }
        }
     
        #endregion
        #endregion

        public AddWindowViewModel()
        {
            _contact = new Contact();
            AddCommand = new lambdaCommand(OnAddCommandExecuted, CanAddCommandExecute);
        }
    }
}
