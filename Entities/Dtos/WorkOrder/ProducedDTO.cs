using Entities.Concrete.Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.WorkOrder
{
    public class ProducedDTO
    {
        public float? Remaining { get; set; }
        public int? Produced { get; set; }
        public UretilecekKoli? ToBeProducedItem { get; set; } = null!;
    }
}
