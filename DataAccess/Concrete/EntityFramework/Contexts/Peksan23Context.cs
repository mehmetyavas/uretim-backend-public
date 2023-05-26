using System;
using System.Collections.Generic;
using Entities.Concrete.Giris;
using Entities.Concrete.Netsis;
using Entities.Concrete.Uretim;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public partial class Peksan23Context : ProjectDbContext
{

    public Peksan23Context(DbContextOptions<Peksan23Context> options, IConfiguration configuration)
        : base(options, configuration)
    {
    }

    public virtual DbSet<EryBoyaSeri> EryBoyaSeris { get; set; }

    public virtual DbSet<EryHammaddeVarilseri> EryHammaddeVarilseris { get; set; }
    public virtual DbSet<MontajHammaddeSeri> MontajHammaddeSeris { get; set; }
    public virtual DbSet<AncTblmachine> AncTblmachines { get; set; }

    public virtual DbSet<AncTblstaff> AncTblstaffs { get; set; }

    public virtual DbSet<RpMontajHmBilgi1> RpMontajHmBilgi1s { get; set; }

    public virtual DbSet<RpUretimSeriTakipMontaj> RpUretimSeriTakipMontajs { get; set; }


    public virtual DbSet<EryMakineTemizlik> EryMakineTemizliks { get; set; }

    public virtual DbSet<EryMakineTemizlikSoru> EryMakineTemizlikSorus { get; set; }
    public virtual DbSet<Esnekyapilandirma> Esnekyapilandirmas { get; set; }

    public virtual DbSet<RpBoyaSeri> RpBoyaSeris { get; set; }

    public virtual DbSet<RpHammaddeVarilseri> RpHammaddeVarilseris { get; set; }

    public virtual DbSet<RpIsemriBilgi> RpIsemriBilgis { get; set; }

    public virtual DbSet<RpUretimSeri> RpUretimSeris { get; set; }

    public virtual DbSet<RpUretimSeriTakip> RpUretimSeriTakips { get; set; }

    public virtual DbSet<Tblisemri> Tblisemris { get; set; }

    public virtual DbSet<Tblisemrirec> Tblisemrirecs { get; set; }

    public virtual DbSet<Tblseritra> Tblseritras { get; set; }

    public virtual DbSet<Tblstsabit> Tblstsabits { get; set; }
    public virtual DbSet<RpKoli> RpKolis { get; set; }

    public virtual DbSet<Tblsthar> Tblsthars { get; set; }

    public virtual DbSet<Tblstokur> Tblstokurs { get; set; }
    public virtual DbSet<RpEtiketTbl> RpEtiketTbls { get; set; }
    public virtual DbSet<PrintLog> PrintLogs { get; set; }

    public virtual DbSet<EryStokListe> EryStokListes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("NetsisDbContext"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IsemriBilgi>(entity =>
        {
            entity.HasNoKey();
            entity.ToSqlQuery("EXEC _ERY_ISEMRI_BILGI1");
            entity.Property(e => e.Keys).HasColumnName("KEYS");
            entity.Property(e => e.Value).HasColumnName("VALUE");
        });



        modelBuilder.Entity<PrintLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("PrintLogs");
            entity.Property(e => e.SerialNo).HasColumnName("SerialNo");
            entity.Property(e => e.MachineId).HasColumnName("MachineId");
            entity.Property(e => e.StaffId).HasColumnName("StaffId");
            entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt").ValueGeneratedOnUpdate().HasDefaultValueSql("GETDATE()");

        });

        modelBuilder.Entity<RpEtiketTbl>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToSqlQuery("EXEC _RP_ETIKET");
            entity.Property(e => e.SeriNo).HasColumnName("SERI_NO");
            entity.Property(e => e.PersonelId).HasColumnName("PERSONEL_ID");
            entity.Property(e => e.IsemriNo).HasColumnName("ISEMRI_NO");
            entity.Property(e => e.MakId).HasColumnName("MAK_ID");
            entity.Property(e => e.Gramaj).HasColumnName("B_AGIRLIK");
            entity.Property(e => e.Dara).HasColumnName("DARA");
            entity.Property(e => e.Net).HasColumnName("NET");
            entity.Property(e => e.Adet).HasColumnName("ADET");
            entity.Property(e => e.StokKodu).HasColumnName("STOK_KODU");
            entity.Property(e => e.Barkod).HasColumnName("BARKOD");
            entity.Property(e => e.StokAdi).HasColumnName("STOK_ADI");
            entity.Property(e => e.OncekiKod).HasColumnName("ONCEKI_KOD");
            entity.Property(e => e.UrunTip).HasColumnName("URUN_TIP");
            entity.Property(e => e.YapKod).HasColumnName("YAP_KOD");
            entity.Property(e => e.AltStokKod).HasColumnName("ALT_SKOD");
            entity.Property(e => e.AltTarih).HasColumnName("ALT_TARIH");
            entity.Property(e => e.AltSaat).HasColumnName("ALT_SAAT");
            entity.Property(e => e.UstStokKod).HasColumnName("UST_SK");
            entity.Property(e => e.UstTarih).HasColumnName("UST_TARIH");
            entity.Property(e => e.UstSaat).HasColumnName("UST_SAAT");
            entity.Property(e => e.RenkAlt).HasColumnName("RENK_ALT");
            entity.Property(e => e.RenkUst).HasColumnName("RENK_UST");
            entity.Property(e => e.LogoRenk).HasColumnName("LOGO_RENK");
            entity.Property(e => e.LogoDesc).HasColumnName("LOGO_DESC");
            entity.Property(e => e.UretTip).HasColumnName("URET_TIP");
            entity.Property(e => e.Date).HasColumnName("DATE");
            entity.Property(e => e.GrossWeight).HasColumnName("GROSS_WEIGHT");
            entity.Property(e => e.Saat).HasColumnName("SAAT");
            entity.Property(e => e.LotNo).HasColumnName("LOT_NO");
            entity.Property(e => e.SealType).HasColumnName("SEAL_TYPE");
            entity.Property(e => e.STarih).HasColumnName("S_TARIH");
            entity.Property(e => e.SSaat).HasColumnName("S_SAAT");

        });


        modelBuilder.Entity<RpKoli>(entity =>
        {
            entity.HasNoKey();
            entity.ToSqlQuery("EXEC RpKoli");
            entity.Property(e => e.StokKodu).HasColumnName("STOK_KODU");
            entity.Property(e => e.SeriNo).HasColumnName("SERI_NO");
            entity.Property(e => e.Bakiye).HasColumnName("BAKIYE");
        });

        modelBuilder.Entity<EryStokListe>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("_ERY_STOK_LISTE");

            entity.Property(e => e.BirimAgirlik)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("BIRIM_AGIRLIK");
            entity.Property(e => e.BoxDimension)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BOX_DIMENSION");
            entity.Property(e => e.CikisSeri)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CIKIS_SERI");
            entity.Property(e => e.GirisSeri)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GIRIS_SERI");
            entity.Property(e => e.Ingisim)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("INGISIM");
            entity.Property(e => e.KalipGozAdedi).HasColumnName("KALIP_GOZ_ADEDI");
            entity.Property(e => e.Kia)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("KIA");
            entity.Property(e => e.Kod4)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("KOD_4");
            entity.Property(e => e.Kod5)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("KOD_5");
            entity.Property(e => e.MaxKg)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MAX_KG");
            entity.Property(e => e.MaximumAd)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MAXIMUM_AD");
            entity.Property(e => e.MinKg)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MIN_KG");
            entity.Property(e => e.MinimumAd)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MINIMUM_AD");
            entity.Property(e => e.OncekiKod)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("ONCEKI_KOD");
            entity.Property(e => e.SeriCikOt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SERI_CIK_OT");
            entity.Property(e => e.SeriGirOt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SERI_GIR_OT");
            entity.Property(e => e.StokAdi)
                .HasMaxLength(4000)
                .HasColumnName("STOK_ADI");
            entity.Property(e => e.StokKodu)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.UreticiKodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("URETICI_KODU");
            entity.Property(e => e.UrunTip)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("URUN_TIP");
        });


        modelBuilder.Entity<Tblsthar>(entity =>
        {
            entity.HasKey(e => e.Inckeyno)
                .HasName("TBLSTHAR_PKEY")
                .IsClustered(false);

            entity.ToTable("TBLSTHAR", tb =>
            {
                tb.HasTrigger("NTR_STHARD");
                tb.HasTrigger("NTR_STHARD_KALEMDETAY");
                tb.HasTrigger("NTR_STHARI");
                tb.HasTrigger("NTR_STHARU");
                tb.HasTrigger("NTR_STHAR_U_INCKEYNO");
                tb.HasTrigger("NTR_TBLSTHAR_IU_YAPKOD");
                tb.HasTrigger("TNF_PICSTHAR");
                tb.HasTrigger("TNF_PICSTHAR_SEVK");
                tb.HasTrigger("_SEVKTRA_DELETE");
                tb.HasTrigger("_SEVKTRA_INSERT");
                tb.HasTrigger("_SEVKTRA_UPDATE");
            });

            entity.HasIndex(e => e.Exportmik, "SQLTRINX_TBLSTHAR_EXPORTMIK_Includes");

            entity.HasIndex(e => new { e.Yapkod, e.StokKodu, e.Fisno }, "TBLSTHAR_IDX_YAPKOD1");

            entity.HasIndex(e => e.StokKodu, "TBLSTHAR_IND_1");

            entity.HasIndex(e => new { e.SubeKodu, e.StokKodu, e.StharTarih, e.Inckeyno }, "TBLSTHAR_IND_2")
                .IsUnique()
                .IsClustered();

            entity.HasIndex(e => new { e.StharGckod, e.StharBgtip, e.Fisno, e.StharAciklama }, "TBLSTHAR_IND_3");

            entity.HasIndex(e => e.StharTarih, "TBLSTHAR_IND_4");

            entity.HasIndex(e => new { e.StharSipnum, e.StharGckod, e.StokKodu }, "TBLSTHAR_IND_5");

            entity.HasIndex(e => new { e.SubeKodu, e.DepoKodu, e.StokKodu, e.StharTarih }, "TBLSTHAR_IND_6");

            entity.HasIndex(e => new { e.StharTestar, e.StharGckod, e.StokKodu }, "TBLSTHAR_IND_7");

            entity.HasIndex(e => new { e.SubeKodu, e.StharFtirsip, e.Fisno, e.StharAciklama, e.Sira }, "TBLSTHAR_IND_8");

            entity.HasIndex(e => new { e.StokKodu, e.StharTarih, e.Inckeyno }, "TBLSTHAR_IND_9").IsUnique();

            entity.HasIndex(e => new { e.StharGckod, e.StharFtirsip, e.IrsaliyeNo, e.DepoKodu }, "TBLSTHAR_IND_DAT");

            entity.HasIndex(e => e.Onaynum, "TBLSTHAR_IND_ONUM");

            entity.HasIndex(e => e.Onaytipi, "TBLSTHAR_IND_OTIP");

            entity.HasIndex(e => new { e.StharGckod, e.StharHtur, e.StharBgtip, e.StharSipnum, e.DepoKodu }, "TBLSTHAR_IND_URTM");

            entity.Property(e => e.Inckeyno).HasColumnName("INCKEYNO");
            entity.Property(e => e.AmbarKabulno)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("AMBAR_KABULNO");
            entity.Property(e => e.BYedek7).HasColumnName("B_YEDEK7");
            entity.Property(e => e.BaglantiNo)
                .HasDefaultValueSql("((0))")
                .HasColumnName("BAGLANTI_NO");
            entity.Property(e => e.CYedek6)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_YEDEK6");
            entity.Property(e => e.Cevrim)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 14)")
                .HasColumnName("CEVRIM");
            entity.Property(e => e.DYedek10)
                .HasColumnType("datetime")
                .HasColumnName("D_YEDEK10");
            entity.Property(e => e.DepoKodu)
                .HasDefaultValueSql("((0))")
                .HasColumnName("DEPO_KODU");
            entity.Property(e => e.Duzeltmetarihi)
                .HasColumnType("datetime")
                .HasColumnName("DUZELTMETARIHI");
            entity.Property(e => e.EczaFatTip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ECZA_FAT_TIP");
            entity.Property(e => e.Ekalan)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EKALAN");
            entity.Property(e => e.Ekalan1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EKALAN1");
            entity.Property(e => e.EkalanNeden)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("EKALAN_NEDEN");
            entity.Property(e => e.Exportmik)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("EXPORTMIK");
            entity.Property(e => e.Exporttype)
                .HasDefaultValueSql("((0))")
                .HasColumnName("EXPORTTYPE");
            entity.Property(e => e.FYedek3)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("F_YEDEK3");
            entity.Property(e => e.FYedek4)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("F_YEDEK4");
            entity.Property(e => e.FYedek5)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("F_YEDEK5");
            entity.Property(e => e.FirmaDovmal)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("FIRMA_DOVMAL");
            entity.Property(e => e.FirmaDovtip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("FIRMA_DOVTIP");
            entity.Property(e => e.FirmaDovtut)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("FIRMA_DOVTUT");
            entity.Property(e => e.Fisno)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("FISNO");
            entity.Property(e => e.Fiyattarihi)
                .HasColumnType("datetime")
                .HasColumnName("FIYATTARIHI");
            entity.Property(e => e.IYedek8).HasColumnName("I_YEDEK8");
            entity.Property(e => e.IrsInckeyno).HasColumnName("IRS_INCKEYNO");
            entity.Property(e => e.IrsaliyeNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("IRSALIYE_NO");
            entity.Property(e => e.IrsaliyeTarih)
                .HasColumnType("datetime")
                .HasColumnName("IRSALIYE_TARIH");
            entity.Property(e => e.Kkmalf)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("KKMALF");
            entity.Property(e => e.Kosulkodu)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("KOSULKODU");
            entity.Property(e => e.Kosultarihi)
                .HasColumnType("datetime")
                .HasColumnName("KOSULTARIHI");
            entity.Property(e => e.LYedek9)
                .HasDefaultValueSql("((0))")
                .HasColumnName("L_YEDEK9");
            entity.Property(e => e.ListeFiat)
                .HasDefaultValueSql("((0))")
                .HasColumnName("LISTE_FIAT");
            entity.Property(e => e.ListeNo)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("LISTE_NO");
            entity.Property(e => e.Mamyapkod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MAMYAPKOD");
            entity.Property(e => e.MuhKodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("MUH_KODU");
            entity.Property(e => e.Olcubr)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OLCUBR");
            entity.Property(e => e.Onaynum).HasColumnName("ONAYNUM");
            entity.Property(e => e.Onaytipi)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("ONAYTIPI");
            entity.Property(e => e.Otvfiyat)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("OTVFIYAT");
            entity.Property(e => e.PlasiyerKodu)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("PLASIYER_KODU");
            entity.Property(e => e.ProjeKodu)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PROJE_KODU");
            entity.Property(e => e.PromasyonKodu)
                .HasDefaultValueSql("((0))")
                .HasColumnName("PROMASYON_KODU");
            entity.Property(e => e.Redmik)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("REDMIK");
            entity.Property(e => e.Redneden)
                .HasDefaultValueSql("((0))")
                .HasColumnName("REDNEDEN");
            entity.Property(e => e.SYedek1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK1");
            entity.Property(e => e.SYedek2)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK2");
            entity.Property(e => e.Satisk1tip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SATISK1TIP");
            entity.Property(e => e.Satisk2tip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SATISK2TIP");
            entity.Property(e => e.Satisk3tip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SATISK3TIP");
            entity.Property(e => e.Satisk4tip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SATISK4TIP");
            entity.Property(e => e.Satisk5tip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SATISK5TIP");
            entity.Property(e => e.Satisk6tip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SATISK6TIP");
            entity.Property(e => e.Sira)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SIRA");
            entity.Property(e => e.StharAciklama)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STHAR_ACIKLAMA");
            entity.Property(e => e.StharBf)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_BF");
            entity.Property(e => e.StharBgtip)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STHAR_BGTIP");
            entity.Property(e => e.StharCarikod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("STHAR_CARIKOD");
            entity.Property(e => e.StharDovfiat)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 15)")
                .HasColumnName("STHAR_DOVFIAT");
            entity.Property(e => e.StharDovtip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("STHAR_DOVTIP");
            entity.Property(e => e.StharFtirsip)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STHAR_FTIRSIP");
            entity.Property(e => e.StharGckod)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STHAR_GCKOD");
            entity.Property(e => e.StharGcmik)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_GCMIK");
            entity.Property(e => e.StharGcmik2)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_GCMIK2");
            entity.Property(e => e.StharHtur)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STHAR_HTUR");
            entity.Property(e => e.StharIaf)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_IAF");
            entity.Property(e => e.StharKdv)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("STHAR_KDV");
            entity.Property(e => e.StharKod1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STHAR_KOD1");
            entity.Property(e => e.StharKod2)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STHAR_KOD2");
            entity.Property(e => e.StharMalfisk)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_MALFISK");
            entity.Property(e => e.StharNf)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_NF");
            entity.Property(e => e.StharOdegun).HasColumnName("STHAR_ODEGUN");
            entity.Property(e => e.StharSatisk)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_SATISK");
            entity.Property(e => e.StharSatisk2)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_SATISK2");
            entity.Property(e => e.StharSipTuru)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STHAR_SIP_TURU");
            entity.Property(e => e.StharSipnum)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("STHAR_SIPNUM");
            entity.Property(e => e.StharTarih)
                .HasColumnType("datetime")
                .HasColumnName("STHAR_TARIH");
            entity.Property(e => e.StharTestar)
                .HasColumnType("datetime")
                .HasColumnName("STHAR_TESTAR");
            entity.Property(e => e.StokKodu)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.StraIrskont)
                .HasDefaultValueSql("((0))")
                .HasColumnName("STRA_IRSKONT");
            entity.Property(e => e.StraSatisk3)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STRA_SATISK3");
            entity.Property(e => e.StraSatisk4)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STRA_SATISK4");
            entity.Property(e => e.StraSatisk5)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STRA_SATISK5");
            entity.Property(e => e.StraSatisk6)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STRA_SATISK6");
            entity.Property(e => e.StraSipkont)
                .HasDefaultValueSql("((0))")
                .HasColumnName("STRA_SIPKONT");
            entity.Property(e => e.SubeKodu).HasColumnName("SUBE_KODU");
            entity.Property(e => e.UpdateKodu)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UPDATE_KODU");
            entity.Property(e => e.VadeTarihi)
                .HasColumnType("datetime")
                .HasColumnName("VADE_TARIHI");
            entity.Property(e => e.Yapkod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("YAPKOD");
            entity.Property(e => e.Yedek11)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("YEDEK11");
            entity.Property(e => e.Yedek12)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("YEDEK12");
            entity.Property(e => e.Yedek13).HasColumnName("YEDEK13");
            entity.Property(e => e.Yedek14).HasColumnName("YEDEK14");
            entity.Property(e => e.Yedek15).HasColumnName("YEDEK15");
            entity.Property(e => e.Yedek16).HasColumnName("YEDEK16");
            entity.Property(e => e.Yedek17)
                .HasColumnType("datetime")
                .HasColumnName("YEDEK17");
            entity.Property(e => e.Yedek18)
                .HasColumnType("datetime")
                .HasColumnName("YEDEK18");
            entity.Property(e => e.Yedek19)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("YEDEK19");
            entity.Property(e => e.Yedek20)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("YEDEK20");
        });

        modelBuilder.Entity<Tblstokur>(entity =>
        {
            entity.HasKey(e => e.Inckeyno).HasName("TBLSTOKURSPKEY");

            entity.ToTable("TBLSTOKURS", tb =>
            {
                tb.HasTrigger("NTR_TBLSTOKURS_IU_YAPKOD");
                tb.HasTrigger("TRG_ISEMRI_RECETE_UPDATE");
            });

            entity.HasIndex(e => new { e.Yapkod, e.UretsonMamul }, "TBLSTOKURS_IDX_YAPKOD1");

            entity.HasIndex(e => e.Inckeyno, "TBLSTOKURS_IND_1").IsUnique();

            entity.HasIndex(e => new { e.UretsonFisno, e.UretsonMamul }, "TBLSTOKURS_IND_2");

            entity.HasIndex(e => new { e.SubeKodu, e.Inckeyno }, "TBLSTOKURS_IND_3");

            entity.HasIndex(e => new { e.SubeKodu, e.UretsonFisno }, "TBLSTOKURS_IND_4");

            entity.Property(e => e.Inckeyno).HasColumnName("INCKEYNO");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ACIKLAMA");
            entity.Property(e => e.Ambarfisno)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("AMBARFISNO");
            entity.Property(e => e.BYedek1).HasColumnName("B_YEDEK1");
            entity.Property(e => e.BYedek2).HasColumnName("B_YEDEK2");
            entity.Property(e => e.BakiyeDepo)
                .HasDefaultValueSql("((0))")
                .HasColumnName("BAKIYE_DEPO");
            entity.Property(e => e.BelgeTipi)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BELGE_TIPI");
            entity.Property(e => e.CYedek1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_YEDEK1");
            entity.Property(e => e.CYedek2)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_YEDEK2");
            entity.Property(e => e.DYedek1)
                .HasColumnType("datetime")
                .HasColumnName("D_YEDEK1");
            entity.Property(e => e.DYedek2)
                .HasColumnType("datetime")
                .HasColumnName("D_YEDEK2");
            entity.Property(e => e.Duzeltmetarihi)
                .HasColumnType("datetime")
                .HasColumnName("DUZELTMETARIHI");
            entity.Property(e => e.Duzeltmeyapankul)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("DUZELTMEYAPANKUL");
            entity.Property(e => e.Eksibakiye)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .HasColumnName("EKSIBAKIYE");
            entity.Property(e => e.FYedek1)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("F_YEDEK1");
            entity.Property(e => e.FYedek2)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("F_YEDEK2");
            entity.Property(e => e.Firedepo).HasColumnName("FIREDEPO");
            entity.Property(e => e.HatKodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("HAT_KODU");
            entity.Property(e => e.IYedek1).HasColumnName("I_YEDEK1");
            entity.Property(e => e.IYedek2).HasColumnName("I_YEDEK2");
            entity.Property(e => e.KayitEdilsin)
                .HasDefaultValueSql("((0))")
                .HasColumnName("KAYIT_EDILSIN");
            entity.Property(e => e.Kayittarihi)
                .HasColumnType("datetime")
                .HasColumnName("KAYITTARIHI");
            entity.Property(e => e.Kayityapankul)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("KAYITYAPANKUL");
            entity.Property(e => e.MaliyetCarpilsin)
                .HasDefaultValueSql("((0))")
                .HasColumnName("MALIYET_CARPILSIN");
            entity.Property(e => e.MamulOlcuBirimi)
                .HasDefaultValueSql("((0))")
                .HasColumnName("MAMUL_OLCU_BIRIMI");
            entity.Property(e => e.Mamulparcala)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("MAMULPARCALA");
            entity.Property(e => e.Oncelik)
                .HasDefaultValueSql("('0')")
                .HasColumnName("ONCELIK");
            entity.Property(e => e.OtoYmamStokKullan)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OTO_YMAM_STOK_KULLAN");
            entity.Property(e => e.ProjeKodu)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PROJE_KODU");
            entity.Property(e => e.ReceteTarihi)
                .HasColumnType("datetime")
                .HasColumnName("RECETE_TARIHI");
            entity.Property(e => e.RefUskFisNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("REF_USK_FIS_NO");
            entity.Property(e => e.SYedek1)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK1");
            entity.Property(e => e.SYedek2)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK2");
            entity.Property(e => e.SYedek3)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK3");
            entity.Property(e => e.SYedek4)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK4");
            entity.Property(e => e.Setno)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("SETNO");
            entity.Property(e => e.SubeKodu).HasColumnName("SUBE_KODU");
            entity.Property(e => e.Tumseviyeler)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TUMSEVIYELER");
            entity.Property(e => e.UretsonDepo)
                .HasDefaultValueSql("((0))")
                .HasColumnName("URETSON_DEPO");
            entity.Property(e => e.UretsonFisno)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("URETSON_FISNO");
            entity.Property(e => e.UretsonMaly1)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("URETSON_MALY1");
            entity.Property(e => e.UretsonMaly2)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("URETSON_MALY2");
            entity.Property(e => e.UretsonMamul)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("URETSON_MAMUL");
            entity.Property(e => e.UretsonMiktar)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("URETSON_MIKTAR");
            entity.Property(e => e.UretsonSipno)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("URETSON_SIPNO");
            entity.Property(e => e.UretsonTarih)
                .HasColumnType("datetime")
                .HasColumnName("URETSON_TARIH");
            entity.Property(e => e.Yapkod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("YAPKOD");
        });





        modelBuilder.Entity<EryBoyaSeri>(entity =>
        {
            entity.ToTable("_ERY_BOYA_SERI");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acik1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACIK1");
            entity.Property(e => e.Harcanan)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("HARCANAN");
            entity.Property(e => e.Isemrino)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ISEMRINO");
            entity.Property(e => e.Oran)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("ORAN");
            entity.Property(e => e.SeriNo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERI_NO");
            entity.Property(e => e.StharGcmik)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_GCMIK");
            entity.Property(e => e.StokKodu)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.UretId).HasColumnName("URET_ID");
            entity.Property(e => e.VarilId).HasColumnName("VARIL_ID");
        });

        modelBuilder.Entity<EryHammaddeVarilseri>(entity =>
        {
            entity.ToTable("_ERY_HAMMADDE_VARILSERI");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Acik1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACIK1");
            entity.Property(e => e.Harcanan)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("HARCANAN");
            entity.Property(e => e.Isemrino)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ISEMRINO");
            entity.Property(e => e.Oran)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("ORAN");
            entity.Property(e => e.SeriNo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERI_NO");
            entity.Property(e => e.StharGcmik)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_GCMIK");
            entity.Property(e => e.StokKodu)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.UretId).HasColumnName("URET_ID");
            entity.Property(e => e.VarilId).HasColumnName("VARIL_ID");
        });



        modelBuilder.Entity<MontajHammaddeSeri>(entity =>
        {
            entity.ToTable("_RP_MONTAJ_HM_BILGI");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.WorkOrder).HasColumnName("ISEMRINO").IsRequired();
            entity.Property(e => e.StockCode).HasColumnName("STOK_KODU").IsRequired();
            entity.Property(e => e.SerialNo).HasColumnName("SERI_NO").IsRequired();
            entity.Property(e => e.Quantity).HasColumnName("MIKTAR").HasColumnType("decimal(18, 8)").IsRequired();
            entity.Property(e => e.Spent).HasColumnName("HARCANAN").HasColumnType("decimal(18, 8)").IsRequired();
            entity.Property(e => e.UretId).HasColumnName("URET_ID").IsRequired();
            entity.Property(e => e.ProductType).HasColumnName("URUN_TIP").IsRequired();
            entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt").HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
        });




        modelBuilder.Entity<RpMontajHmBilgi1>(entity =>
    {
        entity
            .HasNoKey()
            .ToView("_RP_MONTAJ_HM_BILGI1");

        entity.Property(e => e.Degeracik)
            .HasMaxLength(4000)
            .HasColumnName("DEGERACIK");
        entity.Property(e => e.HamKodu)
            .IsRequired()
            .HasMaxLength(35)
            .IsUnicode(false)
            .HasColumnName("HAM_KODU");
        entity.Property(e => e.Hamyapkod)
            .HasMaxLength(15)
            .IsUnicode(false)
            .HasColumnName("HAMYAPKOD");
        entity.Property(e => e.Isemrino)
            .IsRequired()
            .HasMaxLength(15)
            .IsUnicode(false)
            .HasColumnName("ISEMRINO");
        entity.Property(e => e.UrunTip)
            .HasMaxLength(8)
            .IsUnicode(false)
            .HasColumnName("URUN_TIP");
    });

        modelBuilder.Entity<RpUretimSeriTakipMontaj>(entity =>
        {
            entity.ToTable("_RP_URETIM_SERI_TAKIP_MONTAJ");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Harcanan)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("HARCANAN");
            entity.Property(e => e.Isemrino)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("ISEMRINO");
            entity.Property(e => e.SeriNo)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("SERI_NO");
            entity.Property(e => e.StokKodu)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.UretId).HasColumnName("URET_ID");
            entity.Property(e => e.UrunTip)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("URUN_TIP");
            entity.Property(e => e.Yapkod)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("YAPKOD");
        });

        modelBuilder.Entity<AncTblmachine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ANC_TBL");

            entity.ToTable("ANC_TBLMACHINE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(4)
                .HasColumnName("CODE");
            entity.Property(e => e.Description1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION1");
            entity.Property(e => e.Description2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION2");
            entity.Property(e => e.MachineCode)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MACHINE_CODE");
            entity.Property(e => e.ProductionArea)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PRODUCTION_AREA");
        });

        modelBuilder.Entity<AncTblstaff>(entity =>
        {
            entity.ToTable("ANC_TBLSTAFF");

            entity.HasIndex(e => e.StaffCode, "IND_SC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.Gozluk)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("GOZLUK");
            entity.Property(e => e.LastName)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.StaffCode)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("STAFF_CODE");
            entity.Property(e => e.Telefon)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TELEFON");
        });




        modelBuilder.Entity<EryMakineTemizlik>(entity =>
        {
            entity.ToTable("_ERY_MAKINE_TEMIZLIK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Clear).HasColumnName("CLEAR");
            entity.Property(e => e.Desc).HasColumnName("DESC");
            entity.Property(e => e.Machine).HasColumnName("MACHINE");
            entity.Property(e => e.Opentime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("OPENTIME");
            entity.Property(e => e.Query).HasColumnName("QUERY");
            entity.Property(e => e.Question).HasColumnName("QUESTION");
            entity.Property(e => e.Staff).HasColumnName("STAFF");
        });

        modelBuilder.Entity<EryMakineTemizlikSoru>(entity =>
        {
            entity.ToTable("_ERY_MAKINE_TEMIZLIK_SORU");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CalismaYeri).HasColumnName("CALISMA_YERI");
            entity.Property(e => e.Soru)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("SORU");
            entity.Property(e => e.SoruId).HasColumnName("SORU_ID");
        });


        modelBuilder.Entity<Esnekyapilandirma>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("_ESNEKYAPILANDIRMA");

            entity.Property(e => e.Degeracik)
                .HasMaxLength(4000)
                .HasColumnName("DEGERACIK");
            entity.Property(e => e.Degerkod)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DEGERKOD");
            entity.Property(e => e.EngDesc)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ENG_DESC");
            entity.Property(e => e.Ozacik)
                .HasMaxLength(4000)
                .HasColumnName("OZACIK");
            entity.Property(e => e.Ozkod)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("OZKOD");
            entity.Property(e => e.StokKodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.Yapkod)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("YAPKOD");
        });

        modelBuilder.Entity<RpBoyaSeri>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("_RP_BOYA_SERI");

            entity.Property(e => e.Acik1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACIK1");
            entity.Property(e => e.Harcanan)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("HARCANAN");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Isemrino)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ISEMRINO");
            entity.Property(e => e.Oran)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("ORAN");
            entity.Property(e => e.SeriNo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERI_NO");
            entity.Property(e => e.StharGcmik)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_GCMIK");
            entity.Property(e => e.StokKodu)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.UretId).HasColumnName("URET_ID");
            entity.Property(e => e.VarilId).HasColumnName("VARIL_ID");
        });

        modelBuilder.Entity<RpHammaddeVarilseri>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("_RP_HAMMADDE_VARILSERI");

            entity.Property(e => e.Acik1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACIK1");
            entity.Property(e => e.Harcanan)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("HARCANAN");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Isemrino)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ISEMRINO");
            entity.Property(e => e.Oran)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("ORAN");
            entity.Property(e => e.SeriNo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERI_NO");
            entity.Property(e => e.StharGcmik)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("STHAR_GCMIK");
            entity.Property(e => e.StokKodu)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.UretId).HasColumnName("URET_ID");
            entity.Property(e => e.VarilId).HasColumnName("VARIL_ID");
        });

        modelBuilder.Entity<RpIsemriBilgi>(entity =>
        {
            entity.ToTable("_RP_ISEMRI_BILGI");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BirimAgirlik)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("BIRIM_AGIRLIK");
            entity.Property(e => e.Isemrino)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ISEMRINO");
            entity.Property(e => e.Maxad)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("MAXAD");
            entity.Property(e => e.Maxkg)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("MAXKG");
            entity.Property(e => e.Minad)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("MINAD");
            entity.Property(e => e.Minkg)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("MINKG");
            entity.Property(e => e.Serino)
                .IsRequired()
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("SERINO");
            entity.Property(e => e.Tarih)
                .HasColumnType("datetime")
                .HasColumnName("TARIH");
        });

        modelBuilder.Entity<RpUretimSeri>(entity =>
        {
            entity.ToTable("_RP_URETIM_SERI");

            entity.HasIndex(e => e.SeriNo, "UQ___RP_URET__C9285C883A46FB6E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Adet)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ADET");
            entity.Property(e => e.BAgirlik)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("B_AGIRLIK");
            entity.Property(e => e.Brut)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("BRUT");
            entity.Property(e => e.Ciid)
                .HasMaxLength(10)
                .HasColumnName("CIID");
            entity.Property(e => e.Created)
                .HasColumnType("datetime")
                .HasColumnName("CREATED");
            entity.Property(e => e.Dara)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("DARA");
            entity.Property(e => e.EskiId).HasColumnName("ESKI_ID");
            entity.Property(e => e.HataliLot)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("HATALI_LOT");
            entity.Property(e => e.IsemriNo)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("ISEMRI_NO");
            entity.Property(e => e.LotNo)
                .HasMaxLength(50)
                .HasColumnName("LOT_NO");
            entity.Property(e => e.MakId).HasColumnName("MAK_ID");
            entity.Property(e => e.Net)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("NET");
            entity.Property(e => e.PersonelId).HasColumnName("PERSONEL_ID");
            entity.Property(e => e.SeriNo)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("SERI_NO");
            entity.Property(e => e.SipNo)
                .HasMaxLength(15)
                .HasColumnName("SIP_NO");
            entity.Property(e => e.Siralama)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SIRALAMA");
            entity.Property(e => e.StokKodu)
                .IsRequired()
                .HasMaxLength(35)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.SuskHata)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SUSK_HATA");
            entity.Property(e => e.SuskHataMsg)
                .HasMaxLength(200)
                .HasColumnName("SUSK_HATA_MSG");
            entity.Property(e => e.SuskNo)
                .HasMaxLength(15)
                .HasColumnName("SUSK_NO");
            entity.Property(e => e.Tarih)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("TARIH");
            entity.Property(e => e.Terazi)
                .HasMaxLength(50)
                .HasColumnName("TERAZI");
            entity.Property(e => e.UretTip).HasColumnName("URET_TIP");
            entity.Property(e => e.Uretildi).HasColumnName("URETILDI");
            entity.Property(e => e.UretimNo).HasColumnName("URETIM_NO");
            entity.Property(e => e.Vardiya)
                .HasMaxLength(50)
                .HasColumnName("VARDIYA");
            entity.Property(e => e.YapKod)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnName("YAP_KOD");
        });

        modelBuilder.Entity<RpUretimSeriTakip>(entity =>
        {
            entity.ToTable("_RP_URETIM_SERI_TAKIP");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepoKodu).HasColumnName("DEPO_KODU");
            entity.Property(e => e.Harcanan)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("HARCANAN");
            entity.Property(e => e.UretId).HasColumnName("URET_ID");
            entity.Property(e => e.VsSeriNo)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("VS_SERI_NO");
            entity.Property(e => e.VsStokKodu)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("VS_STOK_KODU");
        });

        modelBuilder.Entity<Tblisemri>(entity =>
        {
            entity.HasKey(e => e.Isemrino).HasName("TBLISEMRI_PKEY");

            entity.ToTable("TBLISEMRI", tb =>
                {
                    tb.HasTrigger("NTR_TBLISEMRIBAGLANTI");
                    tb.HasTrigger("NTR_TBLISEMRIREF");
                    tb.HasTrigger("NTR_TBLISEMRI_IU_USTISEMRI");
                    tb.HasTrigger("NTR_TBLISEMRI_IU_YAPKOD");
                    tb.HasTrigger("_TET_TBLISEMRIEK_D");
                });

            entity.HasIndex(e => new { e.Yapkod, e.StokKodu }, "TBLISEMRI_IDX_YAPKOD1");

            entity.HasIndex(e => new { e.Tarih, e.Isemrino }, "TBLISEMRI_IND_1");

            entity.HasIndex(e => e.StokKodu, "TBLISEMRI_IND_2");

            entity.HasIndex(e => new { e.TeslimTarihi, e.StokKodu }, "TBLISEMRI_IND_3");

            entity.HasIndex(e => new { e.StokKodu, e.Yapkod, e.Tepetarih, e.Tepemam, e.Tepeyapkod, e.Tepesipno, e.Tepesipkont }, "TBLISEMRI_IND_4");

            entity.HasIndex(e => e.Onaynum, "TBLISEMRI_IND_ONUM");

            entity.HasIndex(e => e.Onaytipi, "TBLISEMRI_IND_OTIP");

            entity.Property(e => e.Isemrino)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ISEMRINO");
            entity.Property(e => e.Aciklama)
                .HasMaxLength(800)
                .IsUnicode(false)
                .HasColumnName("ACIKLAMA");
            entity.Property(e => e.Asortikod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ASORTIKOD");
            entity.Property(e => e.BaslayabilecegiTarih)
                .HasColumnType("datetime")
                .HasColumnName("BASLAYABILECEGI_TARIH");
            entity.Property(e => e.Calismazamani)
                .HasColumnType("datetime")
                .HasColumnName("CALISMAZAMANI");
            entity.Property(e => e.CikisDepoKodu)
                .HasDefaultValueSql("((0))")
                .HasColumnName("CIKIS_DEPO_KODU");
            entity.Property(e => e.DepoKodu)
                .HasDefaultValueSql("((0))")
                .HasColumnName("DEPO_KODU");
            entity.Property(e => e.Duzeltmetarihi)
                .HasColumnType("datetime")
                .HasColumnName("DUZELTMETARIHI");
            entity.Property(e => e.Duzeltmeyapankul)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("DUZELTMEYAPANKUL");
            entity.Property(e => e.Fasoncari)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("FASONCARI");
            entity.Property(e => e.HatKodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("HAT_KODU");
            entity.Property(e => e.IsemriSira)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ISEMRI_SIRA");
            entity.Property(e => e.Kapali)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("KAPALI");
            entity.Property(e => e.Kayittarihi)
                .HasColumnType("datetime")
                .HasColumnName("KAYITTARIHI");
            entity.Property(e => e.Kayityapankul)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("KAYITYAPANKUL");
            entity.Property(e => e.Miktar)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MIKTAR");
            entity.Property(e => e.Onaynum)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ONAYNUM");
            entity.Property(e => e.Onaytipi)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("ONAYTIPI");
            entity.Property(e => e.Oncelik)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ONCELIK");
            entity.Property(e => e.ProjeKodu)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PROJE_KODU");
            entity.Property(e => e.Refisemrino)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("REFISEMRINO");
            entity.Property(e => e.Revno)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("REVNO");
            entity.Property(e => e.Rework)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("REWORK");
            entity.Property(e => e.RezervasyonStatus)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength()
                .HasColumnName("REZERVASYON_STATUS");
            entity.Property(e => e.Serino)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERINO");
            entity.Property(e => e.Serino2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERINO2");
            entity.Property(e => e.SiparisNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SIPARIS_NO");
            entity.Property(e => e.Sipkont)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SIPKONT");
            entity.Property(e => e.SiraOncelik)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SIRA_ONCELIK");
            entity.Property(e => e.StokKodu)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.Subekodu).HasColumnName("SUBEKODU");
            entity.Property(e => e.Tarih)
                .HasColumnType("datetime")
                .HasColumnName("TARIH");
            entity.Property(e => e.Tepemam)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("TEPEMAM");
            entity.Property(e => e.Tepesipkont)
                .HasDefaultValueSql("((0))")
                .HasColumnName("TEPESIPKONT");
            entity.Property(e => e.Tepesipno)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TEPESIPNO");
            entity.Property(e => e.Tepetarih)
                .HasColumnType("datetime")
                .HasColumnName("TEPETARIH");
            entity.Property(e => e.Tepeyapkod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("TEPEYAPKOD");
            entity.Property(e => e.TeslimTarihi)
                .HasColumnType("datetime")
                .HasColumnName("TESLIM_TARIHI");
            entity.Property(e => e.UskStatus)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength()
                .HasColumnName("USK_STATUS");
            entity.Property(e => e.UstisemriId).HasColumnName("USTISEMRI_ID");
            entity.Property(e => e.Yapkod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("YAPKOD");
            entity.Property(e => e.Yedek1)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("YEDEK1");
            entity.Property(e => e.Yedek2)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("YEDEK2");
            entity.Property(e => e.Yedek3)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("YEDEK3");
            entity.Property(e => e.Yedek4)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("YEDEK4");
            entity.Property(e => e.Yedek5)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("YEDEK5");

            entity.HasOne(d => d.StokKoduNavigation).WithMany(p => p.TblisemriStokKoduNavigations)
                .HasForeignKey(d => d.StokKodu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TBLISEMRI_FKEY");

            entity.HasOne(d => d.TepemamNavigation).WithMany(p => p.TblisemriTepemamNavigations)
                .HasForeignKey(d => d.Tepemam)
                .HasConstraintName("TBLISEMRI_FKEYTMAM");
        });

        modelBuilder.Entity<Tblisemrirec>(entity =>
        {
            entity.HasKey(e => e.Inckeyno).HasName("TBLISEMRIREC_PKEY");

            entity.ToTable("TBLISEMRIREC");

            entity.HasIndex(e => new { e.Mamyapkod, e.MamulKodu }, "TBLISEMRIREC_IDX_YAPKOD1");

            entity.HasIndex(e => new { e.Hamyapkod, e.HamKodu }, "TBLISEMRIREC_IDX_YAPKOD2");

            entity.HasIndex(e => new { e.Isemrino, e.GecFlag, e.MamulKodu, e.HamKodu, e.KayitTarihi, e.Opno, e.UstReceteId }, "TBLISEMRIREC_IND_1").IsUnique();

            entity.HasIndex(e => new { e.Isemrino, e.GecFlag, e.MamulKodu, e.KayitTarihi, e.Opno, e.UstReceteId }, "TBLISEMRIREC_IND_2").IsUnique();

            entity.HasIndex(e => e.HamKodu, "TBLISEMRIREC_IND_5");

            entity.Property(e => e.Inckeyno).HasColumnName("INCKEYNO");
            entity.Property(e => e.Aciklama)
                .HasColumnType("text")
                .HasColumnName("ACIKLAMA");
            entity.Property(e => e.AlternatifPolitikasiAcf)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("ALTERNATIF_POLITIKASI_ACF");
            entity.Property(e => e.AlternatifPolitikasiDat)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("ALTERNATIF_POLITIKASI_DAT");
            entity.Property(e => e.AlternatifPolitikasiMrp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("ALTERNATIF_POLITIKASI_MRP");
            entity.Property(e => e.AlternatifPolitikasiUsk)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("ALTERNATIF_POLITIKASI_USK");
            entity.Property(e => e.BYedek1).HasColumnName("B_YEDEK1");
            entity.Property(e => e.BYedek2).HasColumnName("B_YEDEK2");
            entity.Property(e => e.BaslangicTarihi)
                .HasColumnType("datetime")
                .HasColumnName("BASLANGIC_TARIHI");
            entity.Property(e => e.BilesenAlternatifKodu).HasColumnName("BILESEN_ALTERNATIF_KODU");
            entity.Property(e => e.CYedek1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_YEDEK1");
            entity.Property(e => e.CYedek2)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_YEDEK2");
            entity.Property(e => e.DYedek1)
                .HasColumnType("datetime")
                .HasColumnName("D_YEDEK1");
            entity.Property(e => e.DYedek2)
                .HasColumnType("datetime")
                .HasColumnName("D_YEDEK2");
            entity.Property(e => e.DepoKodu)
                .HasDefaultValueSql("((0))")
                .HasColumnName("DEPO_KODU");
            entity.Property(e => e.Digermal)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("DIGERMAL");
            entity.Property(e => e.FYedek1)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("F_YEDEK1");
            entity.Property(e => e.FYedek2)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("F_YEDEK2");
            entity.Property(e => e.GecFlag).HasColumnName("GEC_FLAG");
            entity.Property(e => e.GecSonBilesen)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("GEC_SON_BILESEN");
            entity.Property(e => e.GecSonMiktar)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("GEC_SON_MIKTAR");
            entity.Property(e => e.GecerlilikTarihi)
                .HasColumnType("datetime")
                .HasColumnName("GECERLILIK_TARIHI");
            entity.Property(e => e.Gecissure)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("GECISSURE");
            entity.Property(e => e.Gecsonbilyapkod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("GECSONBILYAPKOD");
            entity.Property(e => e.Genelmal)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("GENELMAL");
            entity.Property(e => e.HamKodu)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("HAM_KODU");
            entity.Property(e => e.Hamyapkod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("HAMYAPKOD");
            entity.Property(e => e.IYedek1).HasColumnName("I_YEDEK1");
            entity.Property(e => e.IYedek2).HasColumnName("I_YEDEK2");
            entity.Property(e => e.Iscilikmal)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ISCILIKMAL");
            entity.Property(e => e.Isemrino)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ISEMRINO");
            entity.Property(e => e.IstasyonKapasite)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ISTASYON_KAPASITE");
            entity.Property(e => e.Istkodu)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ISTKODU");
            entity.Property(e => e.KayitTarihi)
                .HasColumnType("datetime")
                .HasColumnName("KAYIT_TARIHI");
            entity.Property(e => e.Makinckeyno).HasColumnName("MAKINCKEYNO");
            entity.Property(e => e.MaliyetYuzdesi)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MALIYET_YUZDESI");
            entity.Property(e => e.MamulKodu)
                .IsRequired()
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("MAMUL_KODU");
            entity.Property(e => e.Mamyapkod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MAMYAPKOD");
            entity.Property(e => e.Miktar)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MIKTAR");
            entity.Property(e => e.Miktarsabitle)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MIKTARSABITLE");
            entity.Property(e => e.Oncelik)
                .HasDefaultValueSql("('0')")
                .HasColumnName("ONCELIK");
            entity.Property(e => e.OperasyonUak)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("OPERASYON_UAK");
            entity.Property(e => e.Opkodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("OPKODU");
            entity.Property(e => e.Opmik)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("OPMIK");
            entity.Property(e => e.Opno)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("OPNO");
            entity.Property(e => e.OprBil)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('B')")
                .IsFixedLength()
                .HasColumnName("OPR_BIL");
            entity.Property(e => e.Overlapmik)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("OVERLAPMIK");
            entity.Property(e => e.PlanlamaOrani)
                .HasDefaultValueSql("('0')")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("PLANLAMA_ORANI");
            entity.Property(e => e.ProjeKodu)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PROJE_KODU");
            entity.Property(e => e.SYedek1)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK1");
            entity.Property(e => e.SYedek2)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK2");
            entity.Property(e => e.SYedek3)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK3");
            entity.Property(e => e.SYedek4)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK4");
            entity.Property(e => e.SarfedilenMamul)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("SARFEDILEN_MAMUL");
            entity.Property(e => e.Serino)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERINO");
            entity.Property(e => e.Setupsure)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("SETUPSURE");
            entity.Property(e => e.SonOperasyon)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("SON_OPERASYON");
            entity.Property(e => e.StokMaliyet)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STOK_MALIYET");
            entity.Property(e => e.Uretsure)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("URETSURE");
            entity.Property(e => e.UstReceteId).HasColumnName("UST_RECETE_ID");

            entity.HasOne(d => d.IsemrinoNavigation).WithMany(p => p.Tblisemrirecs)
                .HasForeignKey(d => d.Isemrino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TBLISEMRIREC_FKEY");

            entity.HasOne(d => d.MamulKoduNavigation).WithMany(p => p.Tblisemrirecs)
                .HasForeignKey(d => d.MamulKodu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TBLISEMRIREC_FKEY1");
        });

        modelBuilder.Entity<Tblseritra>(entity =>
        {
            entity.HasKey(e => e.SiraNo).HasName("TBLSERITRAPKEY");

            entity.ToTable("TBLSERITRA", tb =>
                {
                    tb.HasTrigger("NTR_SERITRAD");
                    tb.HasTrigger("TNF_SERITRAEK");
                });

            entity.HasIndex(e => new { e.SubeKodu, e.Depokod }, "SQLTRINX_TBLSERITRA_SUBE_KODU_DEPOKOD_Includes");

            entity.HasIndex(e => e.SeriNo, "TBLSERITRA_IND_1");

            entity.HasIndex(e => new { e.SubeKodu, e.KayitTipi, e.StraInc }, "TBLSERITRA_IND_2");

            entity.HasIndex(e => e.SiraNo, "TBLSERITRA_IND_3").IsUnique();

            entity.HasIndex(e => e.Karsiseri, "TBLSERITRA_IND_4");

            entity.HasIndex(e => new { e.SubeKodu, e.KayitTipi, e.StokKodu, e.SeriNo, e.Gckod, e.SiraNo }, "TBLSERITRA_IND_5").IsUnique();

            entity.HasIndex(e => new { e.SubeKodu, e.KayitTipi, e.Gckod, e.Belgetip, e.Belgeno, e.Haracik }, "TBLSERITRA_IND_6");

            entity.HasIndex(e => e.StokKodu, "TBLSERITRA_IND_7");

            entity.HasIndex(e => e.Yedek1, "TBLSERITRA_IND_8");

            entity.HasIndex(e => new { e.StokKodu, e.Depokod, e.Gckod }, "TBLSERITRA_IND_9");

            entity.HasIndex(e => e.Onaynum, "TBLSERITRA_IND_ONUM");

            entity.HasIndex(e => e.Onaytipi, "TBLSERITRA_IND_OTIP");

            entity.Property(e => e.SiraNo).HasColumnName("SIRA_NO");
            entity.Property(e => e.Acik1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACIK1");
            entity.Property(e => e.Acik2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ACIK2");
            entity.Property(e => e.Acik3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ACIK3");
            entity.Property(e => e.Aciklama4)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ACIKLAMA_4");
            entity.Property(e => e.Aciklama5)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ACIKLAMA_5");
            entity.Property(e => e.AktarilanMiktar)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("AKTARILAN_MIKTAR");
            entity.Property(e => e.Barkod)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("BARKOD");
            entity.Property(e => e.Belgeno)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("BELGENO");
            entity.Property(e => e.Belgetip)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BELGETIP");
            entity.Property(e => e.Cikiskontrol)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CIKISKONTROL");
            entity.Property(e => e.Depokod)
                .HasDefaultValueSql("((0))")
                .HasColumnName("DEPOKOD");
            entity.Property(e => e.Gckod)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GCKOD");
            entity.Property(e => e.Haracik)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("HARACIK");
            entity.Property(e => e.InitMiktar)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("INIT_MIKTAR");
            entity.Property(e => e.Karsiseri)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("KARSISERI");
            entity.Property(e => e.KayitTipi)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("KAYIT_TIPI");
            entity.Property(e => e.Miktar)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MIKTAR");
            entity.Property(e => e.Miktar2)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MIKTAR2");
            entity.Property(e => e.Onaynum).HasColumnName("ONAYNUM");
            entity.Property(e => e.Onaytipi)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("ONAYTIPI");
            entity.Property(e => e.ParentId).HasColumnName("PARENT_ID");
            entity.Property(e => e.SeriNo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERI_NO");
            entity.Property(e => e.SeriNo3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERI_NO_3");
            entity.Property(e => e.SeriNo4)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SERI_NO_4");
            entity.Property(e => e.Sipno)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SIPNO");
            entity.Property(e => e.SonKullanmaTarihi)
                .HasColumnType("datetime")
                .HasColumnName("SON_KULLANMA_TARIHI");
            entity.Property(e => e.StokKodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.StraInc).HasColumnName("STRA_INC");
            entity.Property(e => e.SubeKodu).HasColumnName("SUBE_KODU");
            entity.Property(e => e.Tarih)
                .HasColumnType("datetime")
                .HasColumnName("TARIH");
            entity.Property(e => e.Yedek1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("YEDEK1");
            entity.Property(e => e.Yedek2)
                .HasColumnType("datetime")
                .HasColumnName("YEDEK2");
            entity.Property(e => e.Yedek3)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("YEDEK3");
            entity.Property(e => e.Yedek4).HasColumnName("YEDEK4");
            entity.Property(e => e.Yedek5).HasColumnName("YEDEK5");
            entity.Property(e => e.Yedek6).HasColumnName("YEDEK6");
            entity.Property(e => e.Yedek7)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("YEDEK7");
            entity.Property(e => e.Yedek8)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("YEDEK8");

            entity.HasOne(d => d.StokKoduNavigation).WithMany(p => p.Tblseritras)
                .HasForeignKey(d => d.StokKodu)
                .HasConstraintName("TBLSERITRA_FKEY");
        });

        modelBuilder.Entity<Tblstsabit>(entity =>
        {
            entity.HasKey(e => e.StokKodu).HasName("TBLSTSABIT_PKEY");

            entity.ToTable("TBLSTSABIT", tb =>
                {
                    tb.HasTrigger("NTR_STSABITD_EVRAK");
                    tb.HasTrigger("NTR_STSABITD_HAMMADDE");
                    tb.HasTrigger("NTR_STSABITI");
                    tb.HasTrigger("TNF_STSABITFIYATDEGISIM");
                    tb.HasTrigger("_TBLSTSABIT_D");
                    tb.HasTrigger("_TBLSTSABIT_IU");
                });

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Barkod1 }, "TBLSTSABIT_IND_BARKD1");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Barkod2 }, "TBLSTSABIT_IND_BARKD2");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Barkod3 }, "TBLSTSABIT_IND_BARKD3");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.GrupKodu, e.StokKodu }, "TBLSTSABIT_IND_GRPKOD").IsUnique();

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.StokKodu }, "TBLSTSABIT_IND_KOD").IsUnique();

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Kod1, e.StokKodu }, "TBLSTSABIT_IND_KOD1").IsUnique();

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Kod2, e.StokKodu }, "TBLSTSABIT_IND_KOD2").IsUnique();

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Kod3, e.StokKodu }, "TBLSTSABIT_IND_KOD3").IsUnique();

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Kod4, e.StokKodu }, "TBLSTSABIT_IND_KOD4").IsUnique();

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Kod5, e.StokKodu }, "TBLSTSABIT_IND_KOD5").IsUnique();

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.OncekiKod }, "TBLSTSABIT_IND_ONCKOD");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.SaticiKodu }, "TBLSTSABIT_IND_SATKOD");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.SonrakiKod }, "TBLSTSABIT_IND_SNRKOD");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.StokAdi }, "TBLSTSABIT_IND_STKADI");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.UpdateKodu, e.StokKodu }, "TBLSTSABIT_IND_UPDKOD").IsUnique();

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Onaytipi, e.Onaynum }, "TBLSTSABIT_IND_WKRTIP");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.Onaynum }, "TBLSTSABIT_IND_WRKON");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.SYedek2 }, "TBLSTSAIBT_IND_HACIM");

            entity.HasIndex(e => new { e.IsletmeKodu, e.SubeKodu, e.UreticiKodu }, "TBLSTSAIBT_IND_URTKOD");

            entity.Property(e => e.StokKodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("STOK_KODU");
            entity.Property(e => e.Abckodu)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("ABCKODU");
            entity.Property(e => e.Alfkod)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ALFKOD");
            entity.Property(e => e.AlisDovTip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ALIS_DOV_TIP");
            entity.Property(e => e.AlisFiat1)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ALIS_FIAT1");
            entity.Property(e => e.AlisFiat2)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ALIS_FIAT2");
            entity.Property(e => e.AlisFiat3)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ALIS_FIAT3");
            entity.Property(e => e.AlisFiat4)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ALIS_FIAT4");
            entity.Property(e => e.AlisKdvKodu)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("ALIS_KDV_KODU");
            entity.Property(e => e.Alistaltekkilit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("ALISTALTEKKILIT");
            entity.Property(e => e.AsgariStok)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ASGARI_STOK");
            entity.Property(e => e.AtikUrun)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("ATIK_URUN");
            entity.Property(e => e.AzamiStok)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("AZAMI_STOK");
            entity.Property(e => e.BYedek7).HasColumnName("B_YEDEK7");
            entity.Property(e => e.Baglistokkod)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("BAGLISTOKKOD");
            entity.Property(e => e.Barkod1)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("BARKOD1");
            entity.Property(e => e.Barkod2)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("BARKOD2");
            entity.Property(e => e.Barkod3)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("BARKOD3");
            entity.Property(e => e.BilesenOpKodu)
                .HasDefaultValueSql("((0))")
                .HasColumnName("BILESEN_OP_KODU");
            entity.Property(e => e.Bilesenmi)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BILESENMI");
            entity.Property(e => e.BirimAgirlik)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("BIRIM_AGIRLIK");
            entity.Property(e => e.Boy)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("BOY");
            entity.Property(e => e.CYedek5)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_YEDEK5");
            entity.Property(e => e.CYedek6)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("C_YEDEK6");
            entity.Property(e => e.CikisSeri)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CIKIS_SERI");
            entity.Property(e => e.DYedek10)
                .HasColumnType("datetime")
                .HasColumnName("D_YEDEK10");
            entity.Property(e => e.DepoKodu)
                .HasDefaultValueSql("((0))")
                .HasColumnName("DEPO_KODU");
            entity.Property(e => e.DovAlisFiat)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("DOV_ALIS_FIAT");
            entity.Property(e => e.DovMalFiat)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("DOV_MAL_FIAT");
            entity.Property(e => e.DovSatisFiat)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("DOV_SATIS_FIAT");
            entity.Property(e => e.DovTur)
                .HasDefaultValueSql("((0))")
                .HasColumnName("DOV_TUR");
            entity.Property(e => e.EczaciKari)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ECZACI_KARI");
            entity.Property(e => e.EkonSipMiktar)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("EKON_SIP_MIKTAR");
            entity.Property(e => e.EldeBulMal)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("ELDE_BUL_MAL");
            entity.Property(e => e.En)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("EN");
            entity.Property(e => e.EskiRecete)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ESKI_RECETE");
            entity.Property(e => e.FYedek3)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("F_YEDEK3");
            entity.Property(e => e.FYedek4)
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("F_YEDEK4");
            entity.Property(e => e.FiatBirimi)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('1')")
                .IsFixedLength()
                .HasColumnName("FIAT_BIRIMI");
            entity.Property(e => e.FiktifMam)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("FIKTIF_MAM");
            entity.Property(e => e.Fiyatkodu)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("FIYATKODU");
            entity.Property(e => e.Fiyatsirasi).HasColumnName("FIYATSIRASI");
            entity.Property(e => e.FormulToplami)
                .HasDefaultValueSql("((1))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("FORMUL_TOPLAMI");
            entity.Property(e => e.Genislik)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("GENISLIK");
            entity.Property(e => e.GirisSeri)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GIRIS_SERI");
            entity.Property(e => e.GrupKodu)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("GRUP_KODU");
            entity.Property(e => e.Gumruktarifekodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("GUMRUKTARIFEKODU");
            entity.Property(e => e.IYedek8).HasColumnName("I_YEDEK8");
            entity.Property(e => e.IsletmeKodu).HasColumnName("ISLETME_KODU");
            entity.Property(e => e.KdvOrani)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("KDV_ORANI");
            entity.Property(e => e.KdvTenzilOran)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("KDV_TENZIL_ORAN");
            entity.Property(e => e.Kilit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("KILIT");
            entity.Property(e => e.Kod1)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("KOD_1");
            entity.Property(e => e.Kod2)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("KOD_2");
            entity.Property(e => e.Kod3)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("KOD_3");
            entity.Property(e => e.Kod4)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("KOD_4");
            entity.Property(e => e.Kod5)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("KOD_5");
            entity.Property(e => e.Kodturu)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("KODTURU");
            entity.Property(e => e.KulMik)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("KUL_MIK");
            entity.Property(e => e.LYedek9).HasColumnName("L_YEDEK9");
            entity.Property(e => e.LotSize)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("LOT_SIZE");
            entity.Property(e => e.LotSizecustomer)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("LOT_SIZECUSTOMER");
            entity.Property(e => e.MalFazlasi)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MAL_FAZLASI");
            entity.Property(e => e.Mamulmu)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MAMULMU");
            entity.Property(e => e.MaxIskonto)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MAX_ISKONTO");
            entity.Property(e => e.Miktar)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MIKTAR");
            entity.Property(e => e.MinSipMiktar)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MIN_SIP_MIKTAR");
            entity.Property(e => e.MinSipMiktarcustomer)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("MIN_SIP_MIKTARCUSTOMER");
            entity.Property(e => e.MuhDetaykodu)
                .HasDefaultValueSql("((0))")
                .HasColumnName("MUH_DETAYKODU");
            entity.Property(e => e.Musterisipkilit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("MUSTERISIPKILIT");
            entity.Property(e => e.NakliyetTut)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("NAKLIYET_TUT");
            entity.Property(e => e.OlcuBr1)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("OLCU_BR1");
            entity.Property(e => e.OlcuBr2)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("OLCU_BR2");
            entity.Property(e => e.OlcuBr3)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("OLCU_BR3");
            entity.Property(e => e.Onaynum)
                .HasDefaultValueSql("((0))")
                .HasColumnName("ONAYNUM");
            entity.Property(e => e.Onaytipi)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength()
                .HasColumnName("ONAYTIPI");
            entity.Property(e => e.OncekiKod)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("ONCEKI_KOD");
            entity.Property(e => e.OpsiyonKodu1)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OPSIYON_KODU1");
            entity.Property(e => e.OpsiyonKodu2)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OPSIYON_KODU2");
            entity.Property(e => e.OpsiyonKodu3)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OPSIYON_KODU3");
            entity.Property(e => e.OpsiyonKodu4)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OPSIYON_KODU4");
            entity.Property(e => e.OpsiyonKodu5)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OPSIYON_KODU5");
            entity.Property(e => e.OtomatikUretim)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OTOMATIK_URETIM");
            entity.Property(e => e.Otvtevkifat)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("OTVTEVKIFAT");
            entity.Property(e => e.OzellikKodu1)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OZELLIK_KODU1");
            entity.Property(e => e.OzellikKodu2)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OZELLIK_KODU2");
            entity.Property(e => e.OzellikKodu3)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OZELLIK_KODU3");
            entity.Property(e => e.OzellikKodu4)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OZELLIK_KODU4");
            entity.Property(e => e.OzellikKodu5)
                .HasDefaultValueSql("((0))")
                .HasColumnName("OZELLIK_KODU5");
            entity.Property(e => e.Pay1)
                .HasDefaultValueSql("((1))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("PAY_1");
            entity.Property(e => e.Pay2)
                .HasDefaultValueSql("((1))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("PAY2");
            entity.Property(e => e.Payda1)
                .HasDefaultValueSql("((1))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("PAYDA_1");
            entity.Property(e => e.Payda2)
                .HasDefaultValueSql("((1))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("PAYDA2");
            entity.Property(e => e.Performanskodu)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("PERFORMANSKODU");
            entity.Property(e => e.Planlanacak)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('E')")
                .IsFixedLength()
                .HasColumnName("PLANLANACAK");
            entity.Property(e => e.RiskSuresi)
                .HasDefaultValueSql("((0))")
                .HasColumnName("RISK_SURESI");
            entity.Property(e => e.SYedek1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK1");
            entity.Property(e => e.SYedek2)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK2");
            entity.Property(e => e.SYedek3)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("S_YEDEK3");
            entity.Property(e => e.SabitSipAralik)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SABIT_SIP_ARALIK");
            entity.Property(e => e.Safkod)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SAFKOD");
            entity.Property(e => e.SatDovTip)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SAT_DOV_TIP");
            entity.Property(e => e.SaticiKodu)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("SATICI_KODU");
            entity.Property(e => e.Saticisipkilit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("SATICISIPKILIT");
            entity.Property(e => e.Satinalmakilit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("SATINALMAKILIT");
            entity.Property(e => e.SatisFiat1)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("SATIS_FIAT1");
            entity.Property(e => e.SatisFiat2)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("SATIS_FIAT2");
            entity.Property(e => e.SatisFiat3)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("SATIS_FIAT3");
            entity.Property(e => e.SatisFiat4)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("SATIS_FIAT4");
            entity.Property(e => e.Satiskilit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("SATISKILIT");
            entity.Property(e => e.Satistaltekkilit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("SATISTALTEKKILIT");
            entity.Property(e => e.Sbomvarmi)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("SBOMVARMI");
            entity.Property(e => e.SeriBak)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SERI_BAK");
            entity.Property(e => e.SeriBaslangic)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("SERI_BASLANGIC");
            entity.Property(e => e.SeriCikOt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SERI_CIK_OT");
            entity.Property(e => e.SeriGirOt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SERI_GIR_OT");
            entity.Property(e => e.SeriMik)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SERI_MIK");
            entity.Property(e => e.Seribarkod)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("SERIBARKOD");
            entity.Property(e => e.SipPolitikasi)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SIP_POLITIKASI");
            entity.Property(e => e.SipVerMal)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("SIP_VER_MAL");
            entity.Property(e => e.Siplimitvar)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("SIPLIMITVAR");
            entity.Property(e => e.SonrakiKod)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("SONRAKI_KOD");
            entity.Property(e => e.Sonstokkodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("SONSTOKKODU");
            entity.Property(e => e.StokAdi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("STOK_ADI");
            entity.Property(e => e.Stokmevzuat).HasColumnName("STOKMEVZUAT");
            entity.Property(e => e.SubeKodu).HasColumnName("SUBE_KODU");
            entity.Property(e => e.TeminSuresi)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("TEMIN_SURESI");
            entity.Property(e => e.UpdateKodu)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UPDATE_KODU");
            entity.Property(e => e.UretOlcuBr)
                .HasDefaultValueSql("((0))")
                .HasColumnName("URET_OLCU_BR");
            entity.Property(e => e.UreticiKodu)
                .HasMaxLength(35)
                .IsUnicode(false)
                .HasColumnName("URETICI_KODU");
            entity.Property(e => e.Yapilandir)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('H')")
                .IsFixedLength()
                .HasColumnName("YAPILANDIR");
            entity.Property(e => e.Yapkod)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("YAPKOD");
            entity.Property(e => e.YilTahKulMik)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(28, 8)")
                .HasColumnName("YIL_TAH_KUL_MIK");
            entity.Property(e => e.ZamanBirimi)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("ZAMAN_BIRIMI");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
