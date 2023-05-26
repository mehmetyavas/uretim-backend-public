using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public class CustomException : Exception
    {
        public CustomException(string message, HttpStatusCode status) : base(message)
        {
            Message = base.Message;
            Status = status;
        }
        public CustomException(HttpStatusCode status)
        {
            Status = status;
        }


        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
