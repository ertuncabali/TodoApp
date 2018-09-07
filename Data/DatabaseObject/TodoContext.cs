using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TodoContext : DbContext
    {
        public TodoContext() : base("TodoConnectionString")
        {
            Database.SetInitializer<TodoContext>(new TodoContextInitializer());
        }
        public DbSet<User> User { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<TodoItem> TodoItem { get; set; }

       
    }

}
