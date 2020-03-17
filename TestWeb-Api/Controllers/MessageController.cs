﻿using System;
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
    [Route("Message")]
    public class MessageController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public bool CheckUser(int id)
        {
            using (var context = new AppContext())
            {
                return context.Users.Any(x => x.Id_User == id);
            }
        }
        [HttpPost("sendmessage")]
        public bool SendMessage([FromBody] SendMessageModel model)
        {
            
            MessageModel message = new MessageModel
            {
                Id_User_Sender = model.Id_User_Sender,
                Id_User_Receiver = model.Id_User_Receiver,
                Text = model.Text,
                Viewed = true,
                Checked = false,
                Date_time_send = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString()
            };
            using(var context = new AppContext())
            {
                context.Message.Add(message);
                context.SaveChanges();
                return true;
            }
        }
        [HttpPost("readmessage")]
        public bool ReadMessage([FromBody] ReadMessageModel model)
        {
            
            using(var context = new AppContext())
            {
                var messages = context.Message.Where(x => x.Id_User_Sender == model.Id_User_Sender & x.Id_User_Receiver == model.Id_User_Sender & x.Checked == false & x.Viewed == true).ToList();
                foreach(MessageModel q in messages)
                {
                    q.Checked = true;
                    context.Message.Update(q);
                    context.SaveChanges();
                }
                return true;
                
            }
        }
        [HttpPost("deletemessage")]
        public bool DeleteMessage([FromBody] DeleteMessageModel model)
        {
            
            using(var context = new AppContext())
            {
                var message = context.Message.First(x => x.Id_Message == model.Id_Message & x.Id_User_Sender == model.Id_User_Sender & x.Id_User_Receiver == model.Id_User_Receiver);
                message.Viewed = false;
                context.Message.Update(message);
                context.SaveChanges();
                return true;
            }
        }
        [HttpPost("showmessage")]
        public List<MessageModel> Show_Message([FromBody] ShowMessageModel model)
        {
            using(var context = new AppContext())
            {
                var message = context.Message.Where(x => ((x.Id_User_Sender == model.Id_User_Sender & x.Id_User_Receiver == model.Id_User_Receiver)) || (x.Id_User_Sender == model.Id_User_Receiver & x.Id_User_Receiver == model.Id_User_Sender) & x.Viewed == true).ToList();
                var sortedmessage = from u in message orderby u.Id_Message select u;
                List<MessageModel> newmodel = new List<MessageModel>();
                foreach(var k in sortedmessage)
                {
                    newmodel.Add(k);
                }
                return newmodel;
            }
        }
        [HttpPost("allchats")]
        public List<AllChatsModel> AllChats([FromBody]AllChatUser user)
        {
            List<AllChatsModel> chatsmodel = new List<AllChatsModel>();
            List<User> user_chart = new List<User>();
            using (var context = new AppContext())
            {
                List<MessageModel> list_messages = context.Message.Where(x => (x.Id_User_Sender == user.Id_User || x.Id_User_Receiver == user.Id_User) & x.Viewed == true).ToList();
                foreach (MessageModel u in list_messages)
                {
                    if (u.Id_User_Sender == user.Id_User)
                    {
                        User i_user = context.Users.First(x => x.Id_User == u.Id_User_Receiver);
                        if (!user_chart.Contains(i_user))
                        {
                            user_chart.Add(i_user);
                        }
                    }
                    if(u.Id_User_Receiver == user.Id_User)
                    {
                        User i_user = context.Users.First(x => x.Id_User == u.Id_User_Sender);
                        if (!user_chart.Contains(i_user))
                        {
                            user_chart.Add(i_user);
                        }
                    }
                }
                foreach(User q in user_chart)
                {
                    var message = context.Message.Where(x => (x.Id_User_Sender == q.Id_User & x.Id_User_Receiver == user.Id_User) || (x.Id_User_Sender == user.Id_User & x.Id_User_Receiver == q.Id_User)).ToList();
                    int count_dont_read = message.Count(x => x.Id_User_Receiver == user.Id_User & x.Checked == false);
                    var model = from u in message orderby u.Id_Message select u;
                    var specialmodel = model.Last();
                    AllChatsModel chatmodel = new AllChatsModel
                    {
                        Id_User = q.Id_User,
                        Name = q.Name,
                        Surname = q.Surname,
                        Avatar = q.Avatar,
                        Id_User_sender = specialmodel.Id_User_Sender,
                        Text_Message = specialmodel.Text,
                        Count_Dont_Read = count_dont_read
                    };
                    if (!chatsmodel.Contains(chatmodel))
                    {
                        chatsmodel.Add(chatmodel);
                    }
                    
                }
            }
            chatsmodel.Reverse();
            return chatsmodel;
        }
    }
}
