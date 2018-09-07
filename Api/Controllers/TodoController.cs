using Api.Models;
using Data;
using Data.ApiModel;
using Data.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class TodoController : ApiController
    {
        OperationHelper bus = new OperationHelper();

        // GET api/<controller>
        public Result<List<TodoResponse>> Get()
        {

            var apiResult = new Result<List<TodoResponse>>();
            var todoBus = bus.todoItemBusiness;
            var result = todoBus.ListTasks();


            if (result.IsSuccess)
            {
                var resultData = result.ResultObject;
                var newResultData = resultData.Select(x => new
                TodoResponse
                {
                    Id = x.Id,
                    Status = x.Status.Name,
                    Text = x.Text,
                    UserNameSurname = x.User.Name + " " + x.User.Surname
                }).ToList();
                apiResult = new Result<List<TodoResponse>>(result.ResultType, result.IsSuccess, newResultData);
            }
            return apiResult;
        }
        [Route("api/Todo/{searchText}")]
        public Result<List<TodoResponse>> Get(string searchText)
        {
            var apiResult = new Result<List<TodoResponse>>();
            var todoBus = bus.todoItemBusiness;
            var result = todoBus.ListTasks(searchText);
            if (result.IsSuccess)
            {
                var resultData = result.ResultObject;
                var newResultData = resultData.Select(x => new
                TodoResponse
                {
                    Id = x.Id,
                    Status = x.Status.Name,
                    Text = x.Text,
                    UserNameSurname = x.User.Name + " " + x.User.Surname
                }).ToList();
                apiResult = new Result<List<TodoResponse>>(result.ResultType, result.IsSuccess, newResultData);
            }
            return apiResult;
        }

        // POST api/<controller>
        public Result<bool> Post([FromBody]TodoItem entity)
        {
            var todoBus = bus.todoItemBusiness;
            var result = todoBus.Add(entity);
            return result;
        }

        [Route("api/Todo/status")]
        // POST api/<controller>
        public Result<bool> Post([FromBody]ChangeStatusRequest entity)
        {
            var todoBus = bus.todoItemBusiness;
            var result = todoBus.ChangeStatus(entity.Id, entity.StatusId);
            return result;
        }

    }
}