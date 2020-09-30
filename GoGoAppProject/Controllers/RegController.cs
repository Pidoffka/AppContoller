using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GoGoAppProject.Models;
using GoGoAppProject.ModelsForMethods;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using EntityFrameworkCoreMock;
using Moq;

namespace GoGoAppProject.Controllers
{
    [Route("Reg")]
    public class RegController : Controller
    {
        public IActionResult Index()
        {
            return View("MainPage");
        }
        
        public bool CheckUser([FromBody]string phoneNumber)
        {
            using (var context = new AppContext())
            {
                return context.Users.Any(x => x.phoneNumber == phoneNumber);
            }
        }
        [HttpPost("add_user")]
        public string AddUser([FromBody] AddUserModel authModel)   
        {

            if (CheckUser(authModel.phoneNumber))
            {
                return "null";
            }
            else
            {
                DateTime timeNow = DateTime.Now;
                string cultureInfo = "ru-RU";
                var culture = new CultureInfo(cultureInfo);
                User user = new User()
                {
                    phoneNumber = authModel.phoneNumber,
                    dateSource = timeNow.ToString(culture)
                };
                UserInfoModel info = new UserInfoModel()
                {
                    phoneNumber = user.phoneNumber,
                    name = authModel.name,
                    surname = authModel.surname,
                    gender = authModel.gender,
                    password = authModel.password,
                    city = authModel.city,
                    birthday = authModel.birthday,
                    avatar = authModel.avatar,
                    nickname = authModel.nickname,
                    json = AddJwt(user.phoneNumber),
                };
                UserReviewPoint point = new UserReviewPoint()
                {
                    phoneNumber = user.phoneNumber,
                    reviewPoints = 1000
                };
                try
                {
                    using (var context = new AppContext())
                    {
                        var contextMock = new DbContextMock<AppContext>();
                        context.Users.Add(user);
                        context.UserInfo.Add(info);
                        context.UserReviewPoints.Add(point);
                        context.SaveChanges();
                        return JsonConvert.SerializeObject(user);
                    }
                }
                catch
                {
                    return "Ошибка подключения";
                }
            }

        }

        [HttpPost("check_signin")]
        public string Check_Login([FromBody] AutorizationModel authModel)
        {
            using (var context = new AppContext())
            {
                var user = context.Users.Where(x => x.phoneNumber == authModel.phoneNumber).ToList();
                if(user.Count == 0)
                {
                    return "Неверный номер телефона";
                }
                var user_inf = context.UserInfo.First(x => x.phoneNumber == user[0].phoneNumber);
                if(user_inf.password != authModel.password)
                {
                    return "Неверный пароль";
                }
                else
                {
                    string jsonStr = JsonConvert.SerializeObject(user_inf);
                    return jsonStr;
                }
            }
        }
        [HttpPost("checkJwt")]
        public string CheckJwt([FromBody]JWT jwt)
        {
            using(var appContext = new AppContext())
            {
                var user = appContext.UserInfo.Where(x => x.json == jwt.jsonToken).ToList();
                if(user.Count() == 0)
                {
                    return null;
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
