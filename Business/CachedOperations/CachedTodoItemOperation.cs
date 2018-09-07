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
    public class CachedTodoItemOperation : ITodoItemOperation
    {
        TodoContext db = new TodoContext();
        public Result<bool> Add(TodoItem entity)
        {
            TodoItemOperation tio = new TodoItemOperation();
            return tio.Add(entity);
        }

        public Result<bool> ChangeStatus(int taskId, int status)
        {
            TodoItemOperation tio = new TodoItemOperation();
            return tio.ChangeStatus(taskId, status);
        }

        public Result<List<TodoItem>> ListTasks()
        {
            var result = new Result<List<TodoItem>>(ResultMessage.NoRecord);
            var list = CacheManager.Get<List<TodoItem>>(Keys.TodoItem);
            if (list == null)
            {
                TodoItemOperation so = new TodoItemOperation();
                result = so.ListTasks();
                if (result.IsSuccess)
                {
                    CacheManager.Add(Keys.TodoItem, result.ResultObject);
                    return result;
                }
            }
            if (list.Count > 0)
            {
                result = new Result<List<TodoItem>>(ResultType.Success, true, list);
            }
            return result;
        }

        public Result<List<TodoItem>> ListTasks(string searchText)
        {

            var result = new Result<List<TodoItem>>(ResultMessage.NoRecord);

            var fullListResult = ListTasks();
            if (fullListResult.IsSuccess)
            {
                var fullList = fullListResult.ResultObject;
                var list = fullList.Where(x => x.Text.Contains(searchText)).OrderByDescending(x => x.Id).ToList();
                if (list.Count > 0)
                {
                    result = new Result<List<TodoItem>>(ResultType.Success, true, list);
                }
            }

            return result;
        }

        public Result<List<TodoItem>> ListUserTasks(int userId)
        {
            var result = new Result<List<TodoItem>>(ResultMessage.NoRecord);

            var fullListResult = ListTasks();
            if (fullListResult.IsSuccess)
            {
                var fullList = fullListResult.ResultObject;

                var list = fullList.Where(x => x.UserId == userId).OrderByDescending(x => x.Id).ToList();
                if (list.Count > 0)
                {
                    result = new Result<List<TodoItem>>(ResultType.Success, true, list);
                }
            }



            return result;
        }
    }
}
