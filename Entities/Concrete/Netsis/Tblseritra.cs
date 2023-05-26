using Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete.Netsis;
public partial class Tblseritra : IEntity
{
    public string KayitTipi { get; set; }

    public string SeriNo { get; set; }

    public string StokKodu { get; set; }

    public int SiraNo { get; set; }

    public int? StraInc { get; set; }

    public DateTime? Tarih { get; set; }

    public string Acik1 { get; set; }

    public string Acik2 { get; set; }

    public string Gckod { get; set; }

    public decimal? Miktar { get; set; }

    public decimal? Miktar2 { get; set; }

    public string Belgeno { get; set; }

    public string Belgetip { get; set; }

    public string Haracik { get; set; }

    public short SubeKodu { get; set; }

    public short? Depokod { get; set; }

    public string Sipno { get; set; }

    public string Karsiseri { get; set; }

    public string Yedek1 { get; set; }

    public DateTime? Yedek2 { get; set; }

    public decimal? Yedek3 { get; set; }

    public int? Yedek4 { get; set; }

    public short? Yedek5 { get; set; }

    public byte? Yedek6 { get; set; }

    public string Yedek7 { get; set; }

    public string Yedek8 { get; set; }

    public string Onaytipi { get; set; }

    public int Onaynum { get; set; }

    public string Acik3 { get; set; }

    public string SeriNo3 { get; set; }

    public string SeriNo4 { get; set; }

    public string Aciklama4 { get; set; }

    public string Aciklama5 { get; set; }

    public DateTime? SonKullanmaTarihi { get; set; }

    public int? ParentId { get; set; }

    public decimal? InitMiktar { get; set; }

    public decimal? AktarilanMiktar { get; set; }

    public string Cikiskontrol { get; set; }

    public string Barkod { get; set; }

    public virtual Tblstsabit StokKoduNavigation { get; set; }
}
