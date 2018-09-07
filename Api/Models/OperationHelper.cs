using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business;
using Business.Operations;

namespace Api.Models
{

    public class OperationHelper
    {
        public IStatusOperation statusBusiness = null;
        public ITodoItemOperation todoItemBusiness = null;
        public IUserOperation userBusiness = null;
        public OperationHelper(IStatusOperation _statusBusiness, ITodoItemOperation _todoItemBusiness, IUserOperation _userBusiness)
        {
            statusBusiness = _statusBusiness;
            todoItemBusiness = _todoItemBusiness;
            userBusiness = _userBusiness;
        }

        public OperationHelper()
        {
            statusBusiness = CreateStatusBusiness();
            todoItemBusiness = CreateTodoItemBusiness();
            userBusiness = CreateUserBusiness();
        }

        private IStatusOperation CreateStatusBusiness()
        {
            IStatusOperation result;
            if (WebApiApplication.Company == "Mobven")
            {
                result = new CachedStatusOperation();
            }
            else
            {
                result = new StatusOperation();
            }
            return result;
        }
        private ITodoItemOperation CreateTodoItemBusiness()
        {
            ITodoItemOperation result;
            string company = WebApiApplication.Company;
            if (WebApiApplication.Company == "Mobven")
            {
                result = new CachedTodoItemOperation();
            }
            else
            {
                result = new TodoItemOperation();
            }
            return result;
        }
        private IUserOperation CreateUserBusiness()
        {
            IUserOperation result;
            if (WebApiApplication.Company == "Mobven")
            {
                result = new CachedUserOperation();
            }
            else
            {
                result = new UserOperation();
            }
            return result;
        }

    }
}