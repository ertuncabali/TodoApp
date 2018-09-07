using Data;
using Data.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Api.Models;
using Data.ApiModel;

namespace UI.Controllers
{
    public class TodoController : Controller
    {

        public ActionResult Index()
        {
            
            var result = ApiHelper.Get<Result<List<TodoResponse>>>(ApiHelper.controllers.Todo);
            
            return View(result.ResultObject);
        }
        [HttpPost]
        public ActionResult Index(string searchText)
        {
            var result = ApiHelper.Get<Result<List<TodoResponse>>>(ApiHelper.controllers.Todo+ searchText);
            return View(result.ResultObject);
        }

        public ActionResult Save()
        {
            var result = ApiHelper.Get<Result<List<User>>>(ApiHelper.controllers.Users);
            ViewBag.User = result.ResultObject;
            return View();
        }

        [HttpPost]
        public ActionResult Save(TodoItem data)
        {
            var result = ApiHelper.Post<TodoItem,bool>(ApiHelper.controllers.Todo,data);
            return RedirectToAction("Index");
        }
        public ActionResult ChangeStatus(int Id)
        {
            var newReq = new ChangeStatusRequest { Id = Id, StatusId = 2 };
            var result = ApiHelper.Post<ChangeStatusRequest, bool>(ApiHelper.controllers.Todo+"status", newReq);
            return RedirectToAction("Index");
        }
        
    }
}