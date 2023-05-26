using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.WorkOrder
{
    public class workOrderInfoReqDto:IDto
    {
        public string WorkOrder { get; set; }
        public int MachineId { get; set; }
    }
}
