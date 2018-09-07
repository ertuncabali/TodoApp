using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
   public class Status:Base
    {
        public string Name { get; set; }

        public ICollection<TodoItem> TodoItem { get; set; }
    }
}
