using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Result;
using State.Cache;

namespace Business.Operations
{
    public class CachedStatusOperation : IStatusOperation
    {
        TodoContext db = new TodoContext();

        public Result<List<Status>> ListStatus()
        {
            var result = new Result<List<Status>>(ResultMessage.NoRecord);

            var list = CacheManager.Get<List<Status>>(Keys.Status);
            if (list == null)
            {
                StatusOperation so = new StatusOperation();
                result = so.ListStatus();
                if (result.IsSuccess)
                {
                    CacheManager.Add(Keys.Status, result.ResultObject);
                    return result;
                }
            }

            if (list.Count > 0)
            {
                result = new Result<List<Status>>(ResultType.Success, true, list);
            }

            return result;
        }
        public Result<bool> Add(Status entity)
        {
            StatusOperation so = new StatusOperation();
            return so.Add(entity);
        }
        public Result<bool> Exist(Status entity)
        {
            StatusOperation so = new StatusOperation();
            return so.Exist(entity);
        }


    }
}
