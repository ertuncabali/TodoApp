using Data;
using Data.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
  public  interface IStatusOperation
    {

        Result<bool> Exist(Status entity);
        Result<bool> Add(Status entity);
        Result<List<Status>> ListStatus();
    }
}
