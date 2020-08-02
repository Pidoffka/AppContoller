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
                    double avg = new double();
                    foreach (int t in marks_institution)
                    {
                        sum = sum + t;
                        count = count + 1;
                    }
                    if(count == 0)
                    {
                        avg = 0;
                    }
                    else
                    {
                        avg = (double) sum / count;
                    }
                    
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
        [HttpPost("addconnectioninstitutioncategory")]
        public bool AddConnectionInstitutionCategory([FromBody] AddConnectionInstitutionCategoryModel model)
        {
            using(var context = new AppContext())
            {
                var pastconnection = context.Connection_Institutions_Categories.Where(x => x.Id_Institution == model.Id_Institution).Select(x => x.Id_Category).ToList();
                foreach(var u in model.Id_Categories)
                {
                    if (!pastconnection.Contains(u.Id_Category))
                    {
                        ConnectionInstitutionsCategoriesModel newmodel = new ConnectionInstitutionsCategoriesModel
                        {
                            Id_Institution = model.Id_Institution,
                            Id_Category = u.Id_Category
                        };
                        context.Connection_Institutions_Categories.Add(newmodel);
                        context.SaveChanges();
                    }
                }
                return true;
            }
        }
        [HttpPost("showinstitutionwithcategory")]
        public List<RankedInstitutionsModel> ShowInstitutionWithCategory([FromBody] ShowInstWithCategoryModel model)
        {
            using(var context = new AppContext())
            {
                var all_inst = context.Connection_Institutions_Categories.Select(x => x.Id_Institution).ToList();
                var id_inst = all_inst.Distinct();
                foreach (var k in model.Categories)
                {
                    var id_with_model = context.Connection_Institutions_Categories.Where(x => x.Id_Category == k.Id_Category).Select(x => x.Id_Institution).ToList();
                    foreach(var t in id_inst)
                    {
                        if (!id_with_model.Contains(t))
                        {
                            id_inst = id_inst.Where(x => x != t);
                        }
                    }
                }
                var ranked_list = ShowRankedInstitutions();
                List<RankedInstitutionsModel> newmodel = new List<RankedInstitutionsModel>();
                foreach(var q in id_inst)
                {
                    var inst = ranked_list.First(x => x.Id_Institution == q);
                    newmodel.Add(inst);
                }
                var newmodel_ordered = from u in newmodel orderby u.Mark select u;
                List<RankedInstitutionsModel> newlist = new List<RankedInstitutionsModel>();
                foreach(var q in newmodel_ordered)
                {
                    newlist.Add(q);
                }
                newlist.Reverse();
                return newlist;
            }
        }
        [HttpPost("showinstitutionwithstringsearching")]
        public List<SearchingInstitutionModel> ShowInstitutionWithStringSearching([FromBody]ShowInstWithStringSearchingModel model)
        {
            using(var context = new AppContext())
            {
                var ranked_list = ShowRankedInstitutions();
                List<ForStringSearching> string_searching = new List<ForStringSearching>();
                foreach(var q in ranked_list)
                {
                    char[] spearator = { ' ' };
                    var title = q.Title.Split(spearator);
                    List<string> newlist = new List<string>();
                    foreach(var m in title)
                    {
                        newlist.Add(m);
                    }
                    
                    ForStringSearching string_search = new ForStringSearching
                    {
                        Id_Institution = q.Id_Institution,
                        Titles = newlist,
                        Count_Coincidences = 0
                    };
                    string_searching.Add(string_search);
                }
                var t = model.Request.Split(new char[] { ' ' });
                List<string> request = new List<string>();
                foreach(var q in t)
                {
                    request.Add(q);
                }
                request.Distinct();
                int count_request = request.Count();
                foreach(var q in string_searching)
                {
                    int count = 0;
                    foreach(var e in request)
                    {
                        if (q.Titles.Contains(e))
                        {
                            count = count + 1;
                        }
                    }
                    q.Count_Coincidences = count;
                    
                }
                List<SearchingInstitutionModel> search_model = new List<SearchingInstitutionModel>();
                foreach(var q in string_searching)
                {
                    if ((double)q.Count_Coincidences / count_request >= 0.5)
                    {
                        var index = ranked_list.FindIndex(x => x.Id_Institution == q.Id_Institution);
                        SearchingInstitutionModel newmodel = new SearchingInstitutionModel
                        {
                            Id_Institution = ranked_list[index].Id_Institution,
                            Title = ranked_list[index].Title,
                            Description = ranked_list[index].Description,
                            Placement = ranked_list[index].Placement,
                            Image_Institution = ranked_list[index].Image_Institution,
                            Phone = ranked_list[index].Phone,
                            Email = ranked_list[index].Email,
                            Mark = ranked_list[index].Mark,
                            Count_Marks = ranked_list[index].Count_Marks,
                            Count_Request = q.Count_Coincidences
                        };
                        search_model.Add(newmodel);
                    }
                }
                var s = from u in search_model orderby u.Count_Request select u;
                List<SearchingInstitutionModel> search_mod = new List<SearchingInstitutionModel>();
                foreach(var e in s)
                {
                    search_mod.Add(e);
                }
                return search_mod;

            }
        }
        [HttpPost("institutionchange")]
        public bool ChangeInstitution([FromBody] ChangeInstitutionModel model)
        {
            using(var context = new AppContext())
            {
                var institut = context.Institutions.First(x => x.Id_Institution == model.id);
                if(model.image != "")
                {
                    institut.Image_Institution = model.image;
                }
                if(model.phone != "")
                {
                    institut.Phone = model.phone;
                }
                if(model.placement != "")
                {
                    institut.Placement = model.placement;
                }
                if(model.title != "")
                {
                    institut.Title = model.title;
                }
                if(model.description != "")
                {
                    institut.Description = model.description;
                }
                if(model.email != "")
                {
                    institut.Email = model.email;
                }
                context.SaveChanges();
                return true;
            }
        }

    }
}
