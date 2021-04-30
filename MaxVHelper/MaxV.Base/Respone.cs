using System;
using System.Collections.Generic;
using System.Text;

namespace MaxV.Helper.Entities
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public T ResponeItem { get; set; }
    }
}
