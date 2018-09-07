using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.Result;

namespace Business.Operations
{
    public class TodoItemOperation : ITodoItemOperation
    {
        TodoContext db = new TodoContext();
        public Result<bool> Add(TodoItem entity)
        {

            var result = new Result<bool>();
            entity.StatusId = entity.StatusId == 0 ? 1 : entity.StatusId;
            db.TodoItem.Add(entity);
            try
            {
                db.SaveChanges();
                result = new Result<bool>(ResultType.Success, true, true, ResultMessage.Inserted);
            }
            catch (Exception ex)
            {
                result = new Result<bool>(ResultType.Error, false, false, ResultMessage.Error, ex.InnerException.ToString());
            }

            return result;
        }

        public Result<bool> ChangeStatus(int taskId, int status)
        {
            var result = new Result<bool>();
            var data = db.TodoItem.Where(x => x.Id == taskId).FirstOrDefault();
            if (data != null)
            {
                data.StatusId = status;
                db.SaveChanges();
                result = new Result<bool>(ResultType.Success, true, true, ResultMessage.Updated);
            }
            else
            {
                result = new Result<bool>(ResultType.Error, false, false, ResultMessage.Error, ResultMessage.NoRecord);
            }
            return result;
        }

        public Result<List<TodoItem>> ListTasks()
        {

            var result = new Result<List<TodoItem>>(ResultMessage.NoRecord);
            var list = db.TodoItem.Include("Status").Include("User").OrderByDescending(x => x.Id).ToList();
            if (list.Count > 0)
            {
                result = new Result<List<TodoItem>>(ResultType.Success, true, list);
            }
            return result;
        }

        public Result<List<TodoItem>> ListTasks(string searchText)
        {
            var result = new Result<List<TodoItem>>(ResultMessage.NoRecord);
            var list = db.TodoItem.Include("Status").Include("User").Where(x => x.Text.Contains(searchText)).OrderByDescending(x => x.Id).ToList();
            if (list.Count > 0)
            {
                result = new Result<List<TodoItem>>(ResultType.Success, true, list);
            }
            return result;
        }

        public Result<List<TodoItem>> ListUserTasks(int userId)
        {
            var result = new Result<List<TodoItem>>(ResultMessage.NoRecord);
            var list = db.TodoItem.Include("Status").Include("User").Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToList();
            if (list.Count > 0)
            {
                result = new Result<List<TodoItem>>(ResultType.Success, true, list);
            }
            return result;
        }
    }
}
