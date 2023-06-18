using PhoneBookWeb_Api.Models;

namespace PhoneBookWeb_Api.Interfaces
{
    public interface IDataContact
    {
        /// <summary>
        /// получение всех записей
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Contact>> GetContacts();
        /// <summary>
        /// Получение полной инф о записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Contact> GetOneContact(int id);
        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="contact"></param>
        Task AddContact(Contact contact);
        /// <summary>
        /// удаление записи
        /// </summary>
        /// <param name="contactId"></param>
        Task DeleteContact(int contactId);
        /// <summary>
        /// редактирование записи
        /// </summary>
        /// <param name="persone"></param>
        Task EditContact(Contact contact);
        /// <summary>
        /// сохранение бд
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
