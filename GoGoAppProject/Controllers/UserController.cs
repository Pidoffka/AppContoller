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
    [Route("User")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View("MainPage");
        }

        [HttpPost("add_follower")]
        public string AddFollower([FromBody] FollowerModel model)
        {
            if(model.phoneNumberSender == model.phoneNumberReceiver)
            {
                return "Нельзя добавить самого себя в подписчики";
            }
            else
            {
                using (var context = new AppContext())
                {
                    var followers = context.UserToUser.Where(u => u.phoneNumberSender == model.phoneNumberSender & u.phoneNumberReceiver == model.phoneNumberReceiver).ToList();
                    if (followers.Count == 0)
                    {
                        DateTime timeNow = DateTime.Now;
                        string cultureInfo = "ru-RU";
                        var culture = new CultureInfo(cultureInfo);
                        UserToUserModel newmodel = new UserToUserModel()
                        {
                            phoneNumberSender = model.phoneNumberSender,
                            phoneNumberReceiver = model.phoneNumberReceiver,
                            dateSource = timeNow.ToString(culture),
                            active = true
                        };
                        try
                        {
                            context.UserToUser.Add(newmodel);
                            context.SaveChanges();
                            return "Успешно";
                        }
                        catch
                        {
                            return "Ошибка подключения к серверу";
                        }

                    }
                    else
                    {
                        if (followers[0].active == true)
                        {
                            return "Вы уже подписаны на данного пользователя";
                        }
                        else
                        {
                            DateTime timeNow = DateTime.Now;
                            string cultureInfo = "ru-RU";
                            var culture = new CultureInfo(cultureInfo);
                            followers[0].active = true;
                            followers[0].dateSource = timeNow.ToString(culture);
                            try
                            {
                                context.SaveChanges();
                                return "Успешно";
                            }
                            catch
                            {
                                return "Ошибка подключения к серверу";
                            }

                        }
                    }
                }
            }
            
        }

        [HttpPost("show_follower")]
        public List<UserToUserModel> ShowFollowers([FromBody] ShowFollowersModel model)
        {
            using(var context = new AppContext())
            {
                var followers = context.UserToUser.Where(u => u.phoneNumberSender == model.phoneNumber & u.active == true).ToList();
                return followers;
            }
        }

        [HttpPost("show_your_follower")]
        public List<UserToUserModel> ShowYourFollowers([FromBody] ShowFollowersModel model)
        {
            using(var context = new AppContext())
            {
                var followers = context.UserToUser.Where(u => u.phoneNumberReceiver == model.phoneNumber & u.active == true).ToList();
                return followers;
            }
        }

        [HttpPost("delite_follower")]
        public string DeliteFollower([FromBody] FollowerModel model)
        {
            using(var context = new AppContext())
            {
                var followers = context.UserToUser.Where(u => u.phoneNumberSender == model.phoneNumberSender & u.phoneNumberReceiver == model.phoneNumberReceiver).ToList();
                if(followers.Count == 0)
                {
                    return "Вы не подписаны на данного пользователя";
                }
                else
                {
                    if(followers[0].active == false)
                    {
                        return "Вы не подписаны на данного пользователя";
                    }
                    else
                    {
                        DateTime timeNow = DateTime.Now;
                        string cultureInfo = "ru-RU";
                        var culture = new CultureInfo(cultureInfo);
                        followers[0].active = false;
                        followers[0].dateSource = timeNow.ToString(culture);
                        try
                        {
                            context.SaveChanges();
                            return "Подписка на пользователя успешно удалена";
                        }
                        catch
                        {
                            return "Ошибка подключения к серверу";
                        }
                    }
                }
            }
        }

        [HttpPost("delite_your_follower")]
        public string DeliteYourFollower([FromBody] FollowerModel model)
        {
            using(var context = new AppContext())
            {
                var followers = context.UserToUser.Where(u => u.phoneNumberSender == model.phoneNumberReceiver & u.phoneNumberReceiver == model.phoneNumberSender).ToList();
                if(followers.Count == 0)
                {
                    return "Данный пользователь не подписан на вас";
                }
                else
                {
                    if(followers[0].active == false)
                    {
                        return "Данный пользователь не подписан на вас";
                    }
                    else
                    {
                        DateTime timeNow = DateTime.Now;
                        string cultureInfo = "ru-RU";
                        var culture = new CultureInfo(cultureInfo);
                        followers[0].active = false;
                        followers[0].dateSource = timeNow.ToString(culture);
                        try
                        {
                            context.SaveChanges();
                            return "Подписка данного пользователя на вас была удалена";
                        }
                        catch
                        {
                            return "Ошибка подключения к серверу";
                        }
                    }
                }
            }
        }


        public string AddUserReview([FromBody] AddUserReviewModel model)
        {
            using(var context = new AppContext())
            {
                var userReciever = context.Users.Where(u => u.phoneNumber == model.phoneNumberReciever).ToList();
                if(userReciever.Count == 0)
                {
                    return "Данного пользователя не существует";
                }
                else
                {
                    var userRecieverPoints = context.UserReviewPoints.First(u => u.phoneNumber == userReciever[0].phoneNumber);
                    var userSenderPoints = context.UserReviewPoints.First(u => u.phoneNumber == model.phoneNumberSender);
                    if(userRecieverPoints.reviewPoints >= userSenderPoints.reviewPoints / 100)
                    {
                        if (model.commend == false)
                        {
                            userRecieverPoints.reviewPoints = userRecieverPoints.reviewPoints - userSenderPoints.reviewPoints / 100;
                        }
                        else
                        {
                            userRecieverPoints.reviewPoints = userRecieverPoints.reviewPoints + userSenderPoints.reviewPoints / 100;
                        }
                    }
                    else
                    {
                        if(model.commend == false)
                        {
                            userRecieverPoints.reviewPoints = 0;
                        }
                        else
                        {
                            userRecieverPoints.reviewPoints = userRecieverPoints.reviewPoints + userSenderPoints.reviewPoints / 100;
                        }
                    }
                    var reviews = context.UserReview.Where(u => u.phoneNumberSender == model.phoneNumberSender & u.phoneNumberReceiver == model.phoneNumberReciever).ToList();
                    if(reviews.Count == 0)
                    {
                        UserReviewModel review = new UserReviewModel()
                        {
                            phoneNumberSender = model.phoneNumberSender,
                            phoneNumberReceiver = model.phoneNumberReciever,
                            review = model.review,
                            commend = model.commend
                        };
                        context.UserReview.Add(review);
                        context.SaveChanges();
                        return "Успешно";
                    }
                    else
                    {
                        reviews[0].review = model.review;
                        reviews[0].commend = model.commend;
                        context.SaveChanges();
                        return "Успешно";
                    }
                }
            }
        }
    }
}
