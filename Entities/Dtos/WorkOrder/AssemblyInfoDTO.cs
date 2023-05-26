using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.WorkOrder
{
    public class AssemblyInfoDTO:IDto
    {

        public string? RawMaterialBody { get; set; }
        public string? RawMaterialTop { get; set; }
    }
}
