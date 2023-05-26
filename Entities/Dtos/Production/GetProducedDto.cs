using Core.Entities;
using Entities.Dtos.Production.enjeksiyon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Production
{
    public class GetProducedDto : IDto
    {
        public int Id { get; set; }
        public string SeriNo { get; set; }
        public decimal BAgirlik { get; set; }
        public decimal Dara { get; set; }
        public decimal Net { get; set; }
        public decimal Adet { get; set; }
        public decimal? Brut { get; set; }
        public byte UretTip { get; set; }
        public DateTime Created { get; set; }
        public string SuskNo { get; set; }

    }
}
