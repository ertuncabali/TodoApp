using Api.Models;
using Data.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChangeCompany(string company)
        {
            ApiHelper.Post<CompanyRequest, bool>(ApiHelper.controllers.Company, new CompanyRequest { Name =company });
            return RedirectToAction("Index","Todo");
        }


    }
}