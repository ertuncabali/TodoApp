using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business;
using Api.Models;
using Data;
using Data.Result;

namespace Api.Controllers
{
    public class UserController : ApiController
    {
        OperationHelper bus = new OperationHelper();

        // GET api/<controller>
        public Result<List<User>> Get()
        {
            var userBus = bus.userBusiness;
            var result = userBus.ListUsers();
            return result;
        }
    }
}