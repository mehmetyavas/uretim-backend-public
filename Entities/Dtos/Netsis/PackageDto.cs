using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Netsis
{
    public class PackageDto : IDto
    {
        public string StokKodu { get; set; }
        public string SeriNo { get; set; }
        public decimal? Bakiye { get; set; }
    }
}
