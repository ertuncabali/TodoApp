using Data;
using Data.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
   public  interface ITodoItemOperation
    {
         Result<List<TodoItem>> ListTasks();
         Result<List<TodoItem>> ListTasks(string searchText);
         Result<List<TodoItem>> ListUserTasks(int userId);
         Result<bool> Add(TodoItem entity);
         Result<bool> ChangeStatus(int taskId,int status);
    }
}
