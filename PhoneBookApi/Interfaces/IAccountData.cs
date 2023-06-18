using PhoneBookWeb_Api.Authorize;
using PhoneBookWeb_Api.Models;

namespace PhoneBookWeb_Api.Data
{
    /// <summary>
    /// Взаимодействие с бд касаемо пользователя
    /// </summary>
    public interface IAccountData
    {
        /// <summary>
        /// Поиск пользователя по логину и паролю
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        UserModel GetUserByLogPass(UserModel userModel);
        /// <summary>
        /// Возврат всех пользователей
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserModel> GetUsers();
        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="user"></param>
        void AddUser(User user);
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="user"></param>
        void RemoveUser(User user);
        /// <summary>
        /// Проверка на актуальность Username
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CheckUserName(UserModel user);
        /// <summary>
        /// Поиск пользователя по логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        User GetUserByLogin(string login);
    }
}