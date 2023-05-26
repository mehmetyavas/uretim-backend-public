using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Uretim;

public partial class RpUretimSeriTakipMontaj:IEntity
{
    public int Id { get; set; }

    public string Isemrino { get; set; }

    public string StokKodu { get; set; }

    public string Yapkod { get; set; }

    public string SeriNo { get; set; }

    public decimal Harcanan { get; set; }

    public int UretId { get; set; }

    public string UrunTip { get; set; }
}
