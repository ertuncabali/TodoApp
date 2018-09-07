using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class User: Base
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public ICollection<TodoItem> TodoItem { get; set; }
    }
}
