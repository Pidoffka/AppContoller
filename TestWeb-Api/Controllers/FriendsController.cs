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

        public bool CheckUser(string phone)
        {
            using(var context = new AppContext())
            {
                return context.Users.Any(x => x.Phone_Number == phone);
            }
        }
        
        [HttpPost("send_request")]
        public bool Send_Request([FromBody]SendRequestModel model)
        {
            if (!CheckUser(model.Phone_Number_Sender))
            {
                return false;
            }
            if (!CheckUser(model.Phone_Number_Receiver))
            {
                return false;
            }
            using(var context = new AppContext())
            {
                User user_sender = context.Users.First(x => x.Phone_Number == model.Phone_Number_Sender);
                User user_receiver = context.Users.First(x => x.Phone_Number == model.Phone_Number_Receiver);
                Friend friend = new Friend
                {
                    Id_User_Sender = user_sender.Id_User,
                    Id_User_Receiver = user_receiver.Id_User,
                    Phone_Number_Sender = user_sender.Phone_Number,
                    Phone_Number_Receiver = user_receiver.Phone_Number,
                    Answer = false,
                    Checked = false
                };
                context.Friends.Add(friend);
                context.SaveChanges();
                return true;
            }
        }
        [HttpPost("addfriend")]
        public bool Add_Friend([FromBody] Friend_test friends)
        {
            using (var context = new AppContext())
            {
                var friend = context.Friends.First(x => x.Phone_Number_Sender == friends.phone_number_sender & x.Phone_Number_Receiver == friends.phone_number_receiver & x.Checked == false);
                friend.Answer = friends.yes_not;
                friend.Checked = true;
                context.Friends.Update(friend);
                context.SaveChanges();
                return true;
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
        public List<User> Applications([FromBody] ApplicationModel user)
        {
            using (var context = new AppContext())
            {
                List<User> users = new List<User>();
                var applications = context.Friends.Where(x => x.Checked == false & x.Id_User_Receiver == user.Id_User).ToList();
                foreach(var x in applications)
                {
                    var user_receiver = context.Users.First(u => u.Id_User == x.Id_User_Sender);
                    users.Add(user_receiver);
                }
                return users;
            }
        }
    }
}
