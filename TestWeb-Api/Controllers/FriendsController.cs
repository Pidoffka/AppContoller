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

        public bool CheckUser(int id)
        {
            using(var context = new AppContext())
            {
                return context.Users.Any(x => x.Id_User == id);
            }
        }
        
        [HttpPost("add_friend")]
        public bool Add_friend([FromBody]List<User> user)
        {
            if (!CheckUser(user[0].Id_User))
            {
                return false;
            }
            if (!CheckUser(user[1].Id_User))
            {
                return false;
            }
            Friend friend = new Friend
            {
                Id_User_Sender = user[0].Id_User,
                Id_User_Receiver = user[1].Id_User,
                Phone_Number_Sender = user[0].Phone_Number,
                Phone_Number_Receiver = user[1].Phone_Number,
                Answer = false,
                Checked = false
            };
            using(var context = new AppContext())
            {
                context.Friends.Add(friend);
                context.SaveChanges();
                return true;
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
        [HttpPost("Show_friends")]
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
        [HttpPost("applications")]
        public List<Friend> Applications([FromBody] ApplicationModel user)
        {
            using (var context = new AppContext())
            {
                var applications = context.Friends.Where(x => x.Checked == false & x.Id_User_Receiver == user.Id_User).ToList();
                return applications;
            }
        }
    }
}
