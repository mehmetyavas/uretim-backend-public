using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class Tblisemrirec : IEntity
{
    public string Isemrino { get; set; }

    public string MamulKodu { get; set; }

    public string HamKodu { get; set; }

    public decimal? Miktar { get; set; }

    public string StokMaliyet { get; set; }

    public string Miktarsabitle { get; set; }

    public DateTime? KayitTarihi { get; set; }

    public DateTime? GecerlilikTarihi { get; set; }

    public string GecSonBilesen { get; set; }

    public decimal? GecSonMiktar { get; set; }

    public byte GecFlag { get; set; }

    public DateTime? BaslangicTarihi { get; set; }

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

    public string OperasyonUak { get; set; }

    public string SonOperasyon { get; set; }

    public string OprBil { get; set; }

    public string Opno { get; set; }

    public string Aciklama { get; set; }

    public decimal? Opmik { get; set; }

    public string Istkodu { get; set; }

    public decimal? Setupsure { get; set; }

    public decimal? Uretsure { get; set; }

    public decimal? Gecissure { get; set; }

    public decimal? Overlapmik { get; set; }

    public decimal? Iscilikmal { get; set; }

    public decimal? Digermal { get; set; }

    public decimal? Genelmal { get; set; }

    public int? Oncelik { get; set; }

    public decimal? PlanlamaOrani { get; set; }

    public string AlternatifPolitikasiDat { get; set; }

    public string AlternatifPolitikasiAcf { get; set; }

    public string AlternatifPolitikasiUsk { get; set; }

    public string AlternatifPolitikasiMrp { get; set; }

    public string SarfedilenMamul { get; set; }

    public string Opkodu { get; set; }

    public string Mamyapkod { get; set; }

    public string Hamyapkod { get; set; }

    public string Gecsonbilyapkod { get; set; }

    public int? Makinckeyno { get; set; }

    public int Inckeyno { get; set; }

    public int? UstReceteId { get; set; }

    public decimal? IstasyonKapasite { get; set; }

    public short? DepoKodu { get; set; }

    public string Serino { get; set; }

    public int BilesenAlternatifKodu { get; set; }

    public decimal MaliyetYuzdesi { get; set; }

    public virtual Tblisemri IsemrinoNavigation { get; set; }

    public virtual Tblstsabit MamulKoduNavigation { get; set; }
}
