using System;
using System.Collections.Generic;
using System.Text;

namespace CANAdminApi.Services.Models
{
    public class PaginationList<T> where T : class
    {
        public PaginationList(T data, int total)
        {
            Data = data;
            TotalCount = total;
        }

        public T Data { get; set; }
        public int TotalCount { get; set; }
    }
}
