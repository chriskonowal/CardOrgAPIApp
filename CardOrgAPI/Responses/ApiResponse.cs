using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Responses
{

    public class ApiResponse
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T Value { get; set; }
    }
}
