using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Result;
using State.Cache;

namespace Business.Operations
{
    public class CachedUserOperation : IUserOperation
    {
        TodoContext db = new TodoContext();

        public Result<bool> Add(User entity)
        {
            UserOperation uo = new UserOperation();
            return uo.Add(entity);
        }
        public Result<bool> Exist(User entity)
        {
            UserOperation uo = new UserOperation();
            return uo.Exist(entity);
        }
        public Result<List<User>> ListUsers()
        {
            var result = new Result<List<User>>(ResultMessage.NoRecord);
            var list = CacheManager.Get<List<User>>(Keys.User);
            if (list == null)
            {
                UserOperation so = new UserOperation();
                result = so.ListUsers();
                if (result.IsSuccess)
                {
                    CacheManager.Add(Keys.User, result.ResultObject);
                    return result;
                }
            }
            if (list.Count > 0)
            {
                result = new Result<List<User>>(ResultType.Success, true, list);
            }

            return result;
        }
    }
}
