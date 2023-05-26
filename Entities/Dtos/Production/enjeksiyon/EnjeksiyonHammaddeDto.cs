using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Production.enjeksiyon
{
    public class EnjeksiyonHammaddeDto:IDto
    {
        public int Id { get; set; }
        public int UretId { get; set; }

        public string VsStokKodu { get; set; }

        public string VsSeriNo { get; set; }

        public decimal Harcanan { get; set; }

        public int? DepoKodu { get; set; }
    }
}
