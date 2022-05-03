using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test1.CommonClass
{
    public class ResponseModel
    {
        public string message { set; get; }
        public bool status { set; get; }
        public object responseData { set; get; }
        public int statusCode { set; get; }
    }
}
