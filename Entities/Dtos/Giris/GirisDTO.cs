using Core.Entities;
using Entities.Concrete.Giris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Giris
{
    public class GirisDTO : IDto
    {
        public List<AncTblmachine> Machines { get; set; } = null!;
        public List<AncTblstaff> Staffs { get; set; } = null!;

    }
}
