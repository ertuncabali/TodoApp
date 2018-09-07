using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Result
{
    public class Result<T>
    {
        public ResultType ResultType { get; set; }
        public string ResultMessage { get; set; }
        public string DeveloperMessage { get; set; }
        public T ResultObject { get; set; }
        public bool IsSuccess { get; set; }

        public Result() : this(string.Empty)
        {


        }

        public Result(string Message)
        {
            this.IsSuccess = false;
            this.ResultType = ResultType.Error;
            this.ResultMessage = Message;

        }
        public Result(ResultType resultType, bool IsSucces, T resultObject)
        {
            this.ResultType = resultType;
            this.IsSuccess = IsSucces;
            this.ResultObject = resultObject;
        }

        public Result(ResultType resultType, bool IsSucces, T resultObject, string ResultMessage)
        {
            this.ResultType = resultType;
            this.IsSuccess = IsSucces;
            this.ResultObject = resultObject;
            this.ResultMessage = ResultMessage;
        }
        public Result(ResultType resultType, bool IsSucces, T resultObject, string ResultMessage, string DeveloperMessage)
        {
            this.ResultType = resultType;
            this.IsSuccess = IsSucces;
            this.ResultObject = resultObject;
            this.ResultMessage = ResultMessage;
            this.DeveloperMessage = DeveloperMessage;
        }
    }

   

    
}
