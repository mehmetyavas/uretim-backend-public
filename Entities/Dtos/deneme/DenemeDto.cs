using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.deneme
{
    public class DenemeDto:IDto
    {

        public decimal Miktar { get; set; }
        public decimal KoliMiktari { get; set; }
        public decimal ToplamMiktar { get; set; }
        public string  IsemriNo { get; set; }
    }
}
