using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ApiModel
{
    public class TodoResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserNameSurname { get; set; }
        public string Status { get; set; }
    }
}
