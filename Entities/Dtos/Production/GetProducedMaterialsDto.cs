using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Production
{
    public class GetProducedMaterialsDto : IDto
    {
        public int Id { get; set; }
        public int UretId { get; set; } 
        public string IsemriNo { get; set; } = null;
        public string YapKod { get; set; } = null;
        public string StokKodu { get; set; }
        public string SeriNo { get; set; }
        public decimal Miktar { get; set; }
        public int DepoKod { get; set; }

        public string UrunTip { get; set; } = null;

    }
}
