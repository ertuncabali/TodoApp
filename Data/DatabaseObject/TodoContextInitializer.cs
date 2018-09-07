using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TodoContextInitializer: CreateDatabaseIfNotExists<TodoContext>
    {
        protected override void Seed(TodoContext context)
        {
            context.Status.Add(new Status { Id = 1, Name = "Waiting" });
            context.Status.Add(new Status { Id = 2, Name = "Completed" });

            context.User.Add(new User { Name = "Ertunç" , Surname="Abalı" });
            context.User.Add(new User { Name = "Mustafa" , Surname= "Abalı" });
            context.User.Add(new User { Name = "Ahmet", Surname= "Mehmet" });

            context.SaveChanges();

            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = true;
            base.Seed(context);
        }
    }
}
