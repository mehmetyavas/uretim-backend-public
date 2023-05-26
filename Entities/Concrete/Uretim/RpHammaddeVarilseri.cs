using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Uretim;

public partial class RpHammaddeVarilseri : IEntity
{
    public int Id { get; set; }

    public int VarilId { get; set; }

    public string Isemrino { get; set; }

    public string StokKodu { get; set; }

    public string SeriNo { get; set; }

    public string Acik1 { get; set; }

    public decimal StharGcmik { get; set; }

    public decimal Harcanan { get; set; }

    public int? UretId { get; set; }

    public decimal? Oran { get; set; }
}
