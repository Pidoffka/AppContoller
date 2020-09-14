using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeb_Api.ModelsForMethods
{
    public class AuthOptions
    {
        public const string ISSUER = "GoGoAppProject"; // издатель токена
        const string KEY = "GoGoVoDaThisTokenCrazyUKnow?";   // ключ для шифрации
        public const int LIFETIME = 10; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
