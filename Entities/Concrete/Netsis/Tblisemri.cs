using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class Tblisemri : IEntity
{
    public string Isemrino { get; set; }

    public DateTime? Tarih { get; set; }

    public string StokKodu { get; set; }

    public decimal? Miktar { get; set; }

    public string Aciklama { get; set; }

    public DateTime TeslimTarihi { get; set; }

    public string SiparisNo { get; set; }

    public string Kapali { get; set; }

    public string Yedek1 { get; set; }

    public string Yedek2 { get; set; }

    public string Yedek3 { get; set; }

    public string Yedek4 { get; set; }

    public decimal? Yedek5 { get; set; }

    public string ProjeKodu { get; set; }

    public string Revno { get; set; }

    public int? Oncelik { get; set; }

    public string Refisemrino { get; set; }

    public string Kayityapankul { get; set; }

    public DateTime? Kayittarihi { get; set; }

    public string Duzeltmeyapankul { get; set; }

    public DateTime? Duzeltmetarihi { get; set; }

    public string Yapkod { get; set; }

    public short? Sipkont { get; set; }

    public string Tepemam { get; set; }

    public string Tepeyapkod { get; set; }

    public DateTime? Tepetarih { get; set; }

    public DateTime? Calismazamani { get; set; }

    public string Tepesipno { get; set; }

    public short? Tepesipkont { get; set; }

    public string Onaytipi { get; set; }

    public int? Onaynum { get; set; }

    public short? Subekodu { get; set; }

    public string Rework { get; set; }

    public short? DepoKodu { get; set; }

    public string Serino { get; set; }

    public string Fasoncari { get; set; }

    public short? CikisDepoKodu { get; set; }

    public string Asortikod { get; set; }

    public int? IsemriSira { get; set; }

    public string UskStatus { get; set; }

    public string RezervasyonStatus { get; set; }

    public int? SiraOncelik { get; set; }

    public int? UstisemriId { get; set; }

    public DateTime? BaslayabilecegiTarih { get; set; }

    public string Serino2 { get; set; }

    public string HatKodu { get; set; }

    public virtual Tblstsabit StokKoduNavigation { get; set; }

    public virtual ICollection<Tblisemrirec> Tblisemrirecs { get; } = new List<Tblisemrirec>();

    public virtual Tblstsabit TepemamNavigation { get; set; }
}
