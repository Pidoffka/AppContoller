using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb_Api.Models
{
    public class AuthOptions
    {
        public const string ISSUER = "Pokupashkin&Co"; // издатель токена
        public const string AUDIENCE = "Cliento4ek"; // потребитель токена
        const string KEY = "MiLuchsheChemYandex";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
