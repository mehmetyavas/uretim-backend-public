using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.WorkOrder
{
    public class WorkOrderDTO
    {
        public string Isemrino { get; set; } = null!;
        public string StokKodu { get; set; } = null!;
        public string? Yedek2 { get; set; }
        public string? Yapkod { get; set; }

    }
}
