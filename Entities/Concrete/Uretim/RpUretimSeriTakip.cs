using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Uretim;

public partial class RpUretimSeriTakip : IEntity
{
    public int Id { get; set; }

    public int UretId { get; set; }

    public string VsStokKodu { get; set; }

    public string VsSeriNo { get; set; }

    public decimal Harcanan { get; set; }

    public int? DepoKodu { get; set; }
}
