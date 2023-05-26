using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Uretim
{
    public class PrintLog:IEntity
    {
        public int Id { get; set; }
        public string SerialNo { get; set; }
        public string MachineId { get; set; }
        public string StaffId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
