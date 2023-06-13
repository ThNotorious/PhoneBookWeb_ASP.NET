using PhoneBookWeb.Models;

namespace PhoneBookWeb.Interfaces
{
    public interface IDataContact
    {
        /// <summary>
        /// Получение всех записей
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contact>> GetAll();

        /// <summary>
        /// Получение всех данных кокретного элемента
        /// </summary>
        /// <param name="idContact"></param>
        /// <returns></returns>
        Task<Contact> GetOneElement(int idContact);

        /// <summary>
        /// Добавление элемента
        /// </summary>
        /// <param name="secondName"></param>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<bool> Add(string secondName, string firstName, string middleName, string phoneNumber, string description);

        /// <summary>
        /// Изменение элемента
        /// </summary>
        /// <param name="idContact"></param>
        /// <param name="newSecondName"></param>
        /// <param name="newFirstName"></param>
        /// <param name="newMiddleName"></param>
        /// <param name="newPhoneNumber"></param>
        /// <param name="newDescription"></param>
        /// <returns></returns>
        public Task Changed(int idContact, string newSecondName, string newFirstName, string newMiddleName, string newPhoneNumber, string newDescription);

        /// <summary>
        /// Удаление элемента
        /// </summary>
        /// <param name="idContact"></param>
        /// <returns></returns>
        Task Delete(int idContact);

        /// <summary>
        /// Поиск по фамилии
        /// </summary>
        /// <param name="lastName"></param>
        /// <returns></returns>
        Task<IEnumerable<Contact>> SearchByLastName(string lastName);
    }
}