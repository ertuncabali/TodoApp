using Data;
using Data.Result;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
   public interface IUserOperation
    {
       Result< List<User>> ListUsers();
        Result<bool> Add(User entity);
         Result<bool> Exist(User entity);
    }
}
