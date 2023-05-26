using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Pagination
{
    public class ProducedPaginationDto<T>:IDto
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Count { get; set; }
        public T Data { get; set; }

    }
}
