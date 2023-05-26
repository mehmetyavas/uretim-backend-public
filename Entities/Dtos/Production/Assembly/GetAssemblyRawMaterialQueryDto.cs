using Entities.Concrete.Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Production.Assembly
{
    public class GetAssemblyRawMaterialQueryDto
    {
        public List<MontajHammaddeSeri> BodyMaterials { get; set; } = null!;
        public List<MontajHammaddeSeri> TopMaterials { get; set; }=null!;
    }
}
