using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Result;

namespace Business.Operations
{
    public class StatusOperation : IStatusOperation
    {
        TodoContext db = new TodoContext();

        public Result<List<Status>> ListStatus()
        {

            var result = new Result<List<Status>>(ResultMessage.NoRecord);
            var list = db.Status.ToList();
            if (list.Count > 0)
            {
                result = new Result<List<Status>>(ResultType.Success, true, list);
            }
            return result;

        }
        public Result<bool> Add(Status entity)
        {
            var result = new Result<bool>();

            if (!Exist(entity).ResultObject)
            {
                db.Status.Add(entity);
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
        public Result<bool> Exist(Status entity)
        {
            var exist = db.Status.Any(x => x.Id == entity.Id && x.Name == entity.Name);
            var result = new Result<bool>(ResultType.Success, true, exist);
            return result;
        }


    }
}
