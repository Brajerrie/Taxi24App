using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi24App.Helper
{
    public class ApiResponse
    {
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}