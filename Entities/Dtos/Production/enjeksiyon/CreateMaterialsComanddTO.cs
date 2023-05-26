using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Production.enjeksiyon
{
    public class CreateMaterialsComandDto:IDto
    {
        public string SeriNo { get; set; }
        public int UretTip { get; set; }
    }
}
