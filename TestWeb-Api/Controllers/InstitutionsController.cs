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
    [Route("Institution")]
    public class InstitutionsController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("addinstitution")]
        public bool AddInstitution([FromBody]AddInstitutionModel model)
        {
            using( var context = new AppContext())
            {
                InstitutionsModel institution = new InstitutionsModel
                {
                    Title = model.Title,
                    Description = model.Description,
                    Placement = model.Placement,
                    Image_Institution = model.Image_Institution,
                    Phone = model.Phone,
                    Email = model.Email
                };
                context.Institutions.Add(institution);
                context.SaveChanges();
            }
            return true;
        }
        [HttpPost("showallinstitutions")]
        public List<InstitutionsModel> ShowAllInstitutions()
        {
            using(var context = new AppContext())
            {
                var institutions = context.Institutions.ToList();
                return institutions;
            }
        }
        [HttpPost("addmark")]
        public bool AddMark([FromBody]AddMarkModel model)
        {
            using(var context = new AppContext())
            {
                var id_user_markered = context.Marks_Institutions.Select(u => u.Id_User).ToList();
                if (id_user_markered.Contains(model.Id_User))
                {
                    return false;
                }
                else
                {
                    MarksInstitutionsModel mark_institution = new MarksInstitutionsModel
                    {
                        Id_User = model.Id_User,
                        Id_Institution = model.Id_Institution,
                        Mark = model.Mark
                    };
                    context.Marks_Institutions.Add(mark_institution);
                    context.SaveChanges();
                    return true;
                }
            }
        }
        [HttpPost("showrankedinstitutions")]
        public List<RankedInstitutionsModel> ShowRankedInstitutions()
        {
            using(var context = new AppContext())
            {
                var institutions = context.Institutions.ToList();
                List<RankedInstitutionsModel> ranked_institutions = new List<RankedInstitutionsModel>();
                foreach (var u in institutions)
                {
                    List<int> marks_institution = context.Marks_Institutions.Where(x => x.Id_Institution == u.Id_Institution).Select(q => q.Mark).ToList();
                    var count = 0;
                    int sum = 0;
                    foreach (int t in marks_institution)
                    {
                        sum = sum + t;
                        count = count + 1;
                    }
                    decimal avg = sum / count;
                    RankedInstitutionsModel model = new RankedInstitutionsModel
                    {
                        Id_Institution = u.Id_Institution,
                        Title = u.Title,
                        Description = u.Description,
                        Placement = u.Placement,
                        Image_Institution = u.Image_Institution,
                        Phone = u.Phone,
                        Email = u.Email,
                        Mark = avg,
                        Count_Marks = count
                    };
                    ranked_institutions.Add(model);
                }
                var ranked_list = from q in ranked_institutions orderby q.Mark, q.Count_Marks select q;
                List<RankedInstitutionsModel> newmodel = new List<RankedInstitutionsModel>();
                foreach(var q in ranked_list)
                {
                    newmodel.Add(q);
                }
                newmodel.Reverse();
                return newmodel;
            }
        }
    }
}
