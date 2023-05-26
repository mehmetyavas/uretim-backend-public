using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Netsis
{
    public class SeritraReqDTO:IDto
    {
        public string StokKodu { get; set; }
        public string SeriNo { get; set; }
        public decimal? Miktar { get; set; }
        public string Gckod { get; set; }
    }
}
