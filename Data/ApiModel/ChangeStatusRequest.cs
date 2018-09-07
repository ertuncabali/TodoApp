using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ApiModel
{
    public class ChangeStatusRequest
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
    }
}
