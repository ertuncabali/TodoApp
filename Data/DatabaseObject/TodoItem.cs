using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TodoItem :Base
    {
        public string Text { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
