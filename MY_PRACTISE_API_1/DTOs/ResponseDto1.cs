using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MY_PRACTISE_API_1.DTOs
{
    public class ResponseDto1
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Payload { get; set; }
    }
}
