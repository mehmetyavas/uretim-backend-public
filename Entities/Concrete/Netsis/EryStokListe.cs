using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class EryStokListe:IEntity
{
    public string StokKodu { get; set; }

    public string StokAdi { get; set; }

    public string Ingisim { get; set; }

    public string GirisSeri { get; set; }

    public string CikisSeri { get; set; }

    public string SeriGirOt { get; set; }

    public string SeriCikOt { get; set; }

    public string BoxDimension { get; set; }

    public string OncekiKod { get; set; }

    public short? KalipGozAdedi { get; set; }

    public decimal? BirimAgirlik { get; set; }

    public decimal? Kia { get; set; }

    public string UrunTip { get; set; }

    public string Kod4 { get; set; }

    public string Kod5 { get; set; }

    public decimal? MinimumAd { get; set; }

    public decimal? MaximumAd { get; set; }

    public decimal? MinKg { get; set; }

    public decimal? MaxKg { get; set; }

    public string UreticiKodu { get; set; }
}
