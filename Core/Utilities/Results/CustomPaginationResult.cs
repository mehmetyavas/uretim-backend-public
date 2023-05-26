using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class CustomPaginationResult<T> : IDataResult<T>
    {
        public CustomPaginationResult(T data)
        {
            Data = data;
            Message = PaginationMessages.ListPaged;
            Success = true;
        }

        public bool Success { get; set; }
        public string Message { get; }
        public int TotalRecords { get; set; }
        public T Data { get; }
    }
}
