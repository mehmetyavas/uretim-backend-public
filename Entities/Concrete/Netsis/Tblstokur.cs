using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class Tblstokur:IEntity
{
    public string UretsonFisno { get; set; }

    public DateTime? UretsonTarih { get; set; }

    public string UretsonSipno { get; set; }

    public short? UretsonDepo { get; set; }

    public string UretsonMamul { get; set; }

    public decimal? UretsonMiktar { get; set; }

    public decimal? UretsonMaly1 { get; set; }

    public decimal? UretsonMaly2 { get; set; }

    public short SubeKodu { get; set; }

    public int Inckeyno { get; set; }

    public DateTime? DYedek1 { get; set; }

    public DateTime? DYedek2 { get; set; }

    public string SYedek1 { get; set; }

    public string SYedek2 { get; set; }

    public string SYedek3 { get; set; }

    public string SYedek4 { get; set; }

    public decimal? FYedek1 { get; set; }

    public decimal? FYedek2 { get; set; }

    public short? IYedek1 { get; set; }

    public short? IYedek2 { get; set; }

    public byte? BYedek1 { get; set; }

    public byte? BYedek2 { get; set; }

    public string CYedek1 { get; set; }

    public string CYedek2 { get; set; }

    public string ProjeKodu { get; set; }

    public string Aciklama { get; set; }

    public DateTime? ReceteTarihi { get; set; }

    public string Setno { get; set; }

    public int? Oncelik { get; set; }

    public string Kayityapankul { get; set; }

    public DateTime? Kayittarihi { get; set; }

    public string Duzeltmeyapankul { get; set; }

    public DateTime? Duzeltmetarihi { get; set; }

    public string Yapkod { get; set; }

    public byte? KayitEdilsin { get; set; }

    public byte? MaliyetCarpilsin { get; set; }

    public byte? MamulOlcuBirimi { get; set; }

    public byte? BakiyeDepo { get; set; }

    public byte? OtoYmamStokKullan { get; set; }

    public string Eksibakiye { get; set; }

    public string Mamulparcala { get; set; }

    public string HatKodu { get; set; }

    public string BelgeTipi { get; set; }

    public string RefUskFisNo { get; set; }

    public string Tumseviyeler { get; set; }

    public short Firedepo { get; set; }

    public string Ambarfisno { get; set; }
}
