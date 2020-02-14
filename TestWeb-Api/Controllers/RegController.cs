using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using TestWeb_Api.Models;


namespace TestWeb_Api.Controllers
{
    [Route("Reg")]
    public class RegController : Controller
    {
        public IActionResult Index()
        {
            return View("MainPage");
        }
        [HttpPost("check_user")]
        public bool CheckUser([FromBody]string phoneNumber)
        {
            using (var context = new AppContext())
            {
                return context.Users.Any(x => x.Phone_Number == phoneNumber);
            }
        }
        [HttpPost("add_user")]
        public string AddUser([FromBody] AuthModel authModel)
        {
            if (CheckUser(authModel.Phone_Number))
            {
                return "false";
            }
            User user = new User()
            {
                Name = authModel.Name,
                Surname = authModel.Surname,
                Date_of_Birthday = DateTime.ParseExact(authModel.Date_of_Birthday, "yyyy.MM.dd", CultureInfo.InvariantCulture),
                Gender = authModel.Gender,
                Phone_Number = authModel.Phone_Number,
                Password = authModel.Password,
                JsonToken = AddJwt(authModel.Phone_Number)
            };
            using (var context = new AppContext())
            {
                context.Users.Add(user);        
                context.SaveChanges();
                return JsonConvert.SerializeObject(user);
            }
        }

        [HttpPost("check_signin")]
        public string Check_Login([FromBody] User authModel)
        {
            using (var context = new AppContext())
            {
                var user = context.Users.Where(x => x.Phone_Number == authModel.Phone_Number & x.Password == authModel.Password).ToList();
                if(user.Count() == 0)
                {
                    return "false";
                }
                else
                {
                    string jsonStr = JsonConvert.SerializeObject(user[0]);
                    return jsonStr;
                }
            }
        }
        [HttpPost("checkJwt")]
        public string CheckJwt(string token)
        {
            using(var appContext = new AppContext())
            {
                var user = appContext.Users.Where(x => x.JsonToken == token).ToList();
                if(user.Count() == 0)
                {
                    return "null";
                }
                else
                {
                    string jsonStr = JsonConvert.SerializeObject(user[0]);
                    return jsonStr;
                }
            }
        }
        private string AddJwt(string phoneNumber)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: phoneNumber,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var access_token = encodedJwt;
            return access_token;
        }
    }
}
