using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Result;

namespace Business.Operations
{
    public class UserOperation : IUserOperation
    {
        TodoContext db = new TodoContext();

        public Result<bool> Add(User entity)
        {
            var result = new Result<bool>();

            if (!Exist(entity).ResultObject)
            {
                db.User.Add(entity);
                try
                {
                    db.SaveChanges();
                    result = new Result<bool>(ResultType.Success, true, true, ResultMessage.Inserted);
                }
                catch (Exception ex)
                {
                    result = new Result<bool>(ResultType.Error, false, false, ResultMessage.Error, ex.InnerException.ToString());
                }
            }
            else
            {
                result = new Result<bool>(ResultType.Error, false, false, ResultMessage.Error);

            }
            return result;
        }

        public Result<bool> Exist(User entity)
        {
            var exist = db.User.Any(x => x.Name == entity.Name && x.Surname == entity.Surname);
            var result = new Result<bool>(ResultType.Success, true, exist);
            return result;
        }

        public Result<List<User>> ListUsers()
        {
            var result = new Result<List<User>>(ResultMessage.NoRecord);
            var list = db.User.ToList();
            if (list.Count > 0)
            {
                result = new Result<List<User>>(ResultType.Success, true, list);
            }
            return result;
        }
    }
}
