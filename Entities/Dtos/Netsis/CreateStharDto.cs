using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Netsis
{
    public class CreateStharDto:IDto
    {
        public string UretsonFisNo { get; set; }
        public string StockCode { get; set; }
        public decimal StharGcmik { get; set; }
        public decimal StharGcmik2 { get; set; }
        public string GcKod { get; set; }
        public int DepoKod { get; set; }
        public string StharAciklama { get; set; }
        public string StharHtur { get; set; }
        public string StharBgTip { get; set; }
        public string StharSipNum { get; set; }
    }
}
