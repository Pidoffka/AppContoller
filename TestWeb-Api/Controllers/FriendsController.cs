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
    [Route("Friend")]
    public class FriendsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        public bool CheckUser1(int id_User)
        {
            using(var context = new AppContext())
            {
                return context.Users.Any(x => x.Id_User == id_User);
            }
        }
        [HttpPost("add_friend")]
        public bool Add_friend(List<User> users)
        {
            foreach (User x in users)
            {
                if (!CheckUser1(x.Id_User))
                {
                    return false;
                }
            }
            Follower follower = new Follower()
            {
                Id_User_Sender = users[0].Id_User,
                Id_User_Receiver = users[1].Id_User,
                Checked = false,
                Viewed = true,
                Answer = false
                
            };
            using (var context = new AppContext())
            {
                context.Followers.Add(follower);
                context.SaveChanges();
            }
            return true;
        }
        [HttpPost("Checked_friend")]
        public bool Check_friend(List<User> users, bool Yes_Not)
        {
            using (var context = new AppContext())
            {
                var followers = context.Followers.Where(y => y.Id_User_Sender == users[0].Id_User & y.Id_User_Receiver == users[1].Id_User & y.Checked == false).ToList();
                if (followers.Count == 0)
                {
                    return false;
                }
                foreach (Follower x in followers)
                {
                    if (x.Checked == false)
                    {
                        
                        x.Checked = true;
                        context.Followers.Update(x);
                        context.SaveChanges();
                        if (Yes_Not)
                        {
                            
                            Friend friend = new Friend()
                            {
                                Id_User_Sender = x.Id_User_Sender,
                                Id_User_Receiver = x.Id_User_Receiver,
                                Checked = true,
                                Viewed = true,
                                Answer = true
                            };
                            x.Answer = true;
                            context.Followers.Update(x);
                            context.Friends.Add(friend);
                            context.SaveChanges();
                            return true;
                        }
                        
                    }
                }

            }
            return false;
        }
    }
}
