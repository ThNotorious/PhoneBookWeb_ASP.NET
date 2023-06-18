using PhoneBookWpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWpf.StaticComponents
{
    public static class User
    {
        public static UserModel _userModel { get; set; }
        public static TokenResponse _tokenResponse { get; set; }
        public static ApiRequests _apiRequests { get; set; } = new ApiRequests(new HttpClient(), Paths.BaseUrl);
    }
}
