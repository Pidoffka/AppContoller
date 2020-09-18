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
    [Route("Building")]
    public class BuildingController : Controller
    {
        public IActionResult Index()
        {
            return View("MainPage");
        }

        [HttpPost("change_favorite")]
        public string ChangeFavorite([FromBody] ChangeFavoriteModel model)
        {
            try
            {
                using (var context = new AppContext())
                {
                    var favorites = context.FavoritePlaces.Where(u => u.phoneNumber == model.phoneNumber & u.buildingId == model.buildingId).ToList();
                    if (favorites.Count == 0)
                    {
                        FavoritePlace favorite = new FavoritePlace()
                        {
                            phoneNumber = model.phoneNumber,
                            buildingId = model.buildingId,
                            attribute = 1
                        };

                        context.FavoritePlaces.Add(favorite);
                        context.SaveChanges();
                        return "true";
                    }
                    else
                    {
                        if (favorites[0].attribute == 0)
                        {
                            favorites[0].attribute = 1;
                        }
                        else
                        {
                            favorites[0].attribute = 0;
                        }
                        context.SaveChanges();
                        return "true";
                    }
                }
            }
            catch
            {
                return "Ошибка подключения";
            }
            
        }

        [HttpPost("view_favorite")]
        public List<BuildingInfoModel> ViewFavorite([FromBody]ViewFavoriteModel model)
        {
            using(var context = new AppContext())
            {
                var favorites = context.FavoritePlaces.Where(u => u.phoneNumber == model.phoneNumber & u.attribute == 1).ToList();
                List<BuildingInfoModel> newmodel = new List<BuildingInfoModel>();
                foreach(var favorite in favorites)
                {
                    Building newbuilding = context.Buildings.First(u => u.buildingId == favorite.buildingId);
                    BuildingInfoModel building = context.BuildingInfo.First(u => u.buildingId == newbuilding.buildingId);
                    newmodel.Add(building);
                }
                return newmodel;
            }
        }
    }
}
