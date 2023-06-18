using Microsoft.IdentityModel.Tokens;
using PhoneBookWeb_Api.Interfaces;
using PhoneBookWeb_Api.Interfaces.Password;
using PhoneBookWeb_Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PhoneBookWeb_Api.Authorize
{
    /// <summary>
    /// Класс для взаимодействия с JWT авторизацией
    /// </summary>
    public class JWT : IJWT
    {
        private readonly IConfiguration _configuration;
        private readonly IDataContact _contact;

        /// <summary>
        /// секретный ключ
        /// </summary>        
        /// <summary>       
        private byte[] key;

        /// <summary>
        /// получение секретного ключа для JWT из конфигурации
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="contact"></param>
        public JWT(IConfiguration configuration, IDataContact contact)
        {
            _configuration = configuration;
            var secretKey = configuration.GetValue<string>("JwtSettings:SecretKey");
            key = Encoding.UTF8.GetBytes(secretKey);
            _contact = contact;
        }

        /// <summary>
        /// хэширование пароля
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            using (var sha256 = new SHA256Managed())
            {
                var argHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(argHash);
            }
        }

        /// <summary>
        /// cоздается ключ безопасности
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public string GenerateJWT(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userInfo.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
