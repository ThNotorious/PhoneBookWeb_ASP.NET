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
    internal class EditWindowViewModel : ViewModel
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
        #region Изменение Persone
       
        public ICommand EditCommand { get; }
       
        private bool CanEditCommandExecute(object parameter) => true;
       
        private async void OnEditCommandExecuted(object parameter)
        {
            if (Contact.SecondName == null || Contact.FirstName == null || Contact.MiddleName == null ||
            Contact.PhoneNumber == null || Contact.Description == null)
            {
                ErrorContent = "Заполните все поля."; return;
            }
            try
            {
                await User._apiRequests.ContactEdit(_contact, User._tokenResponse.token);
                EditWindow editWindow = (EditWindow)parameter;
                editWindow.Close();
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
        #endregion
       
        public EditWindowViewModel(Contact persone)
        {
            _contact = persone;
            EditCommand = new lambdaCommand(OnEditCommandExecuted, CanEditCommandExecute);
        }
    }
}
