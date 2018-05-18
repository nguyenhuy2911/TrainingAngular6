using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TraningApp.WebAPI.Models
{
    public class Response <T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
