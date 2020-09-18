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
    [Route("Events")]
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View("MainPage");
        }
        [HttpPost("add_event")]
        public string AddEvent([FromBody] AddEventmodel model)
        {
            DateTime timeNow = DateTime.Now;
            string cultureInfo = "ru-RU";
            var culture = new CultureInfo(cultureInfo);
            Event event_ = new Event()
            {
                phoneNumberCreator = model.phoneNumberCreator,
                dateCreated = timeNow.ToString(culture)
            };
            EventInfo eventinfo = new EventInfo()
            {
                phoneNumberCreator = event_.phoneNumberCreator,
                dateCreated = event_.dateCreated,
                title = model.title,
                description = model.description,
                gender = model.gender,
                personNumber = model.personNumber,
                meetingDate = model.meetingDate,
                userRegistred = 1,
                done = false
            };
            EventBuilding connect = new EventBuilding()
            {
                phoneNumberCreator = event_.phoneNumberCreator,
                dateCreated = event_.dateCreated,
                buildingId = model.buildingId
            };
            UserInEvent user = new UserInEvent()
            {
                phoneNumberCreator = event_.phoneNumberCreator,
                dateCreated = event_.dateCreated,
                phoneNumber = model.phoneNumberCreator,
                takePart = true
            };
            try
            {
                using (var context = new AppContext())
                {
                    context.Events.Add(event_);
                    context.EventsInfo.Add(eventinfo);
                    context.EventsBuilding.Add(connect);
                    context.UsersInEvents.Add(user);
                    context.SaveChanges();
                    return "true";
                }
            }
            catch
            {
                return "Ошибка подключения";
            }
        }

        [HttpPost("view_events")]
        public List<EventInfo> ViewEvents()
        {
            List<EventInfo> events = new List<EventInfo>();
            using(var context = new AppContext())
            {
                var list = context.Events.ToList();
                foreach(var item in list)
                {
                    var event_ = context.EventsInfo.First(u => u.phoneNumberCreator == item.phoneNumberCreator & u.dateCreated == item.dateCreated);
                    if(event_.done == false)
                    {
                        events.Add(event_);
                    }
                }
            }
            return events;
        }

        [HttpPost("take_part_in_event")]
        public string AddUserToEvent([FromBody]AddUserToEventModel model)
        {
            using(var context = new AppContext())
            {
                var event_ = context.Events.Where(u => u.phoneNumberCreator == model.phoneNumberCreator & u.dateCreated == model.dateCreated).ToList();
                if(event_.Count == 0)
                {
                    return "Данного события не существует";
                }
                else
                {
                    var check = context.UsersInEvents.Where(u => u.phoneNumberCreator == model.phoneNumberCreator & u.dateCreated == model.dateCreated & u.phoneNumber == model.phoneNumber).ToList();
                    var maxcount = context.EventsInfo.First(u => u.dateCreated == event_[0].dateCreated & u.phoneNumberCreator == event_[0].phoneNumberCreator);
                    if(maxcount.userRegistred == maxcount.personNumber)
                    {
                        return "Уже зарегистрировано максимальное количество человек";
                    }
                    else
                    {
                        if(check.Count > 0)
                        {
                            return "Вы уже подали завку на участие в событии";
                        }
                        else
                        {
                            UserInEvent user = new UserInEvent()
                            {
                                phoneNumber = model.phoneNumber,
                                phoneNumberCreator = model.phoneNumberCreator,
                                dateCreated = model.dateCreated,
                                takePart = false
                            };
                            context.UsersInEvents.Add(user);
                            context.SaveChanges();
                            return "true";
                        }
                    }
                }
                
            }
        }

        [HttpPost("check_take_part_in_event")]
        public string CheckUserToIvent([FromBody]CheckUserInEventModel model)
        {
            using(var context = new AppContext())
            {
                var event_ = context.UsersInEvents.Where(u => u.phoneNumberCreator == model.phoneNumberCreator & u.dateCreated == model.dateCreated).ToList();
                if(event_.Count == 0)
                {
                    return "Данного события не существует";
                }
                else
                {
                    if(model.phoneNumberUser != event_[0].phoneNumberCreator)
                    {
                        return "У вас недостаточно прав";
                    }
                    else
                    {
                        var person = event_.Where(u => u.phoneNumber == model.phoneNumber).ToList();
                        if(person.Count == 0)
                        {
                            return "Данный пользователь не зарегистрирован в данном событии";
                        }
                        else
                        {
                            if(person[0].takePart == true)
                            {
                                return "Вы уже подтвердили участие данного пользователя";
                            }
                            else
                            {
                                person[0].takePart = true;
                                context.SaveChanges();
                                return "true";
                            }
                            
                        }
                    }
                }
            }
        }

    }
}
