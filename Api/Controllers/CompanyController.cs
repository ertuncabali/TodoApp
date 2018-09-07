using Data.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class CompanyController : ApiController
    {
        // POST api/<controller>
        public bool Post([FromBody]CompanyRequest request)
        {
            WebApiApplication.Company = request.Name;
            return true;
        }
    }
}