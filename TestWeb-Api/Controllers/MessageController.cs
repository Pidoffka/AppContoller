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
        public bool SendMessage([FromBody] MessageModel model)
        {
            if (!CheckUser(model.Id_User_Sender))
            {
                return false;
            }
            if (!CheckUser(model.Id_User_Receiver))
            {
                return false;
            }
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
        public bool ReadMessage([FromBody] MessageModel model)
        {
            if (!CheckUser(model.Id_User_Sender))
            {
                return false;
            }
            if (!CheckUser(model.Id_User_Receiver))
            {
                return false;
            }
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
        public bool DeleteMessage([FromBody] MessageModel model)
        {
            if (!CheckUser(model.Id_User_Sender))
            {
                return false;
            }
            if (!CheckUser(model.Id_User_Receiver))
            {
                return false;
            }
            using(var context = new AppContext())
            {
                var message = context.Message.First(x => x.Date_time_send == model.Date_time_send & x.Id_User_Sender == model.Id_User_Sender & x.Id_User_Receiver == model.Id_User_Receiver);
                message.Viewed = false;
                context.Message.Update(message);
                context.SaveChanges();
                return true;

            }
        }

        public List<MessageModel> Show_Message([FromBody] ShowMessageModel model)
        {
            using(var context = new AppContext())
            {
                var list = context.Message.Where(x => (x.Id_User_Sender == model.Id_User_Sender & x.Id_User_Receiver == model.Id_User_Receiver) || (x.Id_User_Sender == model.Id_User_Receiver & x.Id_User_Receiver == model.Id_User_Sender)).ToList();
                return list;
            }
        }
    }
}
