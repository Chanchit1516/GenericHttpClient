using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ServiceTest.API.DTOs
{
    public class ResponseMessage<T>
    {
        public HttpStatusCode status { get; set; }
        public string message { get; set; }
        public List<Error> errorsList { get; set; }
        public T data { get; set; }

        public ResponseMessage()
        {
            status = HttpStatusCode.OK;
            message = string.Empty;
        }

        public ResponseMessage(T model)
        {
            status = HttpStatusCode.OK;
            message = string.Empty;
            data = model;
        }

        public ResponseMessage(T model, Error error)
        {
            status = HttpStatusCode.OK;
            message = string.Empty;
            data = model;
            errorsList = new List<Error> { error };
        }

        public ResponseMessage(Exception ex)
        {
            status = HttpStatusCode.InternalServerError;
            message = ex.Message;
            data = default(T);
            errorsList = new List<Error>
            {
                new Error(ex.InnerException?.ToString(), ex.StackTrace.ToString())
            };
        }
    }

    public sealed class Error
    {
        public string code { get; }
        public string description { get; }

        public Error(string code, string description = "")
        {
            this.code = code;
            this.description = description;
        }
    }
}
