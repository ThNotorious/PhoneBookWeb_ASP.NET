using PhoneBookWpf.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhoneBookWpf.StaticComponents
{
    public class ApiRequests
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public ApiRequests(HttpClient client, string baseUrl)
        {
            _httpClient = client;
            _baseUrl = baseUrl;
        }
        /// <summary>
        /// Залогинивание
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<TokenResponse> LoginRequest(UserModel user)
        {
            var json = JsonSerializer.Serialize(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/Account/Login", data);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<TokenResponse>(responseJson);
                return token;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            else
            {
                throw new HttpResponseException(response.StatusCode);
            }
        }
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<TokenResponse> RegisterRequest(UserModel user)
        {
            var json = JsonSerializer.Serialize(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/Account/Register", data);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<TokenResponse>(responseJson);
                return token;
            }
            else if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }
            else
            {
                throw new HttpResponseException(response.StatusCode);
            }
        }
        /// <summary>
        /// Получение всех записей
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Contact/GetContacts");

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // имена свойств должны быть в формате camelCase
                };
                var contacts = await JsonSerializer.DeserializeAsync<IEnumerable<Contact>>(responseStream, options);
                return contacts;
            }
            return null;
        }
        /// <summary>
        /// Получение полной инф о записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Contact> GetOneContactById(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Contact/GetOneContactById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // имена свойств должны быть в формате camelCase
                };
                var contact = await JsonSerializer.DeserializeAsync<Contact>(responseStream, options);
                return contact;
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            else
            {
                throw new HttpResponseException(response.StatusCode);
            }
        }
        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> ContactAdd(Contact contact, string token)
        {
            string body = JsonSerializer.Serialize(contact);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsync($"{_baseUrl}/Contact/ContactAdd", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            else
            {
                throw new HttpResponseException(response.StatusCode);
            }
        }
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="contact"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> ContactEdit(Contact contact, string token)
        {
            string body = JsonSerializer.Serialize(contact);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PutAsync($"{_baseUrl}/Contact/EditContact", content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            else
            {
                throw new HttpResponseException(response.StatusCode);
            }
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ContactDelete(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/Contact/DeletePersone/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            else
            {
                throw new HttpResponseException(response.StatusCode);
            }
        }
        /// <summary>
        /// Получение всех user
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserModel>> GetUsers(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Account/GetUsers");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // имена свойств должны быть в формате camelCase
                };
                var users = await JsonSerializer.DeserializeAsync<IEnumerable<UserModel>>(responseStream, options);
                return users;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            else
            {
                throw new HttpResponseException(response.StatusCode);
            }
        }
        /// <summary>
        /// Удаление user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        public async Task<bool> DeleteUser(string userName, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/Account/DeleteUser/{userName}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            else
            {
                throw new HttpResponseException(response.StatusCode);
            }
        }
    }
}