using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.WorkOrder
{
    public class WorkOrderInfoDTO:IDto
    {
        public Dictionary<string, string> WorkOrderInfo { get; set; } = null!;
        public string? AssemblyBody { get; set; } = null!;
        public string? AssemblyTop { get; set; }
    }
}
