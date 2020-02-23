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
using TestWeb_Api.Models_for_Metods;

namespace TestWeb_Api.Controllers
{
    [Route("Friend")]
    public class FriendsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public User Find_user([FromBody]Find_user_model phone)
        {
            using (var context = new AppContext())
            {
                User user = context.Users.First(x => x.Phone_Number == phone.phone_number);
                return user;
            }
        }
        
        [HttpPost("add_friend")]
        public void Add_friend([FromBody]List<User> users)
        {
            using(var context = new AppContext())
            {
                var User_Sender = context.Users.First(x => x.Phone_Number == users[0].Phone_Number);
                var User_Receiver = context.Users.First(x => x.Phone_Number == users[1].Phone_Number);
                Friend friend = new Friend
                {
                    Id_User_Sender = User_Sender.Id_User,
                    Id_User_Receiver = User_Receiver.Id_User,
                    Phone_Number_Sender = User_Sender.Phone_Number,
                    Phone_Number_Receiver = User_Receiver.Phone_Number,
                    Answer = false,
                    Checked = false
                };
                context.Friends.Add(friend);
                context.SaveChanges();
            }
            
        }
        [HttpPost("Checked_friend")]
        public void Check_friend([FromBody] Friend_test friends)
        {
            using (var context = new AppContext())
            {
                var friend = context.Friends.First(x => x.Phone_Number_Sender == friends.phone_number_sender & x.Phone_Number_Receiver == friends.phone_number_receiver & x.Checked == false);
                friend.Answer = friends.yes_not;
                friend.Checked = true;
                context.Friends.Update(friend);
                context.SaveChanges();
            }
        }
        public List<User> Show_friends([FromBody] User user)
        {
            using(var context = new AppContext())
            {
                var list = context.Friends.Where(x => x.Answer == true & (x.Phone_Number_Sender == user.Phone_Number || x.Phone_Number_Receiver == user.Phone_Number)).ToList();
                List<User> users = new List<User>();
                int z;
                foreach(Friend s in list)
                {
                    if(s.Phone_Number_Sender == user.Phone_Number)
                    {
                        z = s.Id_User_Receiver;
                    }
                    else
                    {
                        z = s.Id_User_Sender;
                    }
                    User friend_user = context.Users.First(x => x.Id_User == z);
                    users.Add(friend_user);
                }
                return users;
            }
            
        }
    }
}
