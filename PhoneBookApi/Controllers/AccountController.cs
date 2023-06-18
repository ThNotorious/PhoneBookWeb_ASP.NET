using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBookWeb_Api.Authorize;
using PhoneBookWeb_Api.Data;
using PhoneBookWeb_Api.Interfaces;
using PhoneBookWeb_Api.Interfaces.Password;
using PhoneBookWeb_Api.Models;

namespace PhoneBookWeb_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IDataContact _contacts;
       
        private readonly IAccountData _accountData;
       
        private readonly IJWT _jwt;
      
       
        public AccountController(IDataContact contacts, IAccountData accountData, IJWT jwt)
        {
            _contacts = contacts;
            _accountData = accountData;
            _jwt = jwt;
        }
      
        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            var user = _accountData.GetUserByLogPass(login);

            if (user != null)
            {
                var tokenString = _jwt.GenerateJWT(user);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }
       
        /// <summary>
        /// Регистрация в системе
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("Register")]
        [HttpPost]
        public IActionResult Register([FromBody] UserModel login)
        {
            bool check = _accountData.CheckUserName(login);
            if (check == false)
            {
                login.Role = "User";
                User user = new() { Username = login.Username, PasswordHash = _jwt.HashPassword(login.Password), Role = login.Role };
                _accountData.AddUser(user);
                var tokenString = _jwt.GenerateJWT(login);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Conflict();
            }
        }
       
        /// <summary>
        /// Запрос всех зарегистрированных пользователей
        /// </summary>
        /// <returns></returns>
        [Route("GetUsers")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IEnumerable<UserModel> GetUsers()
        {
            return _accountData.GetUsers();
        }

        /// <summary>
        /// Удаление user
        /// </summary>
        /// <param name="id"></param>
        [Route("DeleteUser/{userName}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(string userName)
        {
            var user = _accountData.GetUserByLogin(userName);
            if (user == null) { return NotFound(); }
            _accountData.RemoveUser(user);
            return Ok();
        }
    }
}
