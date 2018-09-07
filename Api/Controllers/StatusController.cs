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
    public class StatusController : ApiController
    {
        OperationHelper bus = new OperationHelper();

        // GET api/<controller>
        public Result<List<Status>> Get()
        {
            var userBus = bus.statusBusiness;
            var result = userBus.ListStatus();
            return result;
        }
    }
}