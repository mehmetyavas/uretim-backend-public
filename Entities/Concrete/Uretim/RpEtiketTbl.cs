using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Uretim
{
    public class RpEtiketTbl : IEntity
    {
        public int Id { get; set; }
        public string SeriNo { get; set; }
        public int PersonelId { get; set; }
        public string IsemriNo { get; set; }
        public int MakId { get; set; }
        public decimal Gramaj { get; set; }
        public decimal Dara { get; set; }
        public decimal Net { get; set; }
        public int Adet { get; set; }
        public string StokKodu { get; set; }
        public string Barkod { get; set; }
        public string StokAdi { get; set; }
        public string OncekiKod { get; set; }
        public string UrunTip { get; set; }
        public string YapKod { get; set; }
        public string? AltStokKod { get; set; }
        public string? AltTarih { get; set; }
        public string? AltSaat { get; set; }
        public string? UstStokKod { get; set; }
        public string? UstTarih { get; set; }
        public string? UstSaat { get; set; }
        public string? RenkAlt { get; set; }
        public string? RenkUst { get; set; }
        public string? LogoRenk { get; set; }
        public string? LogoDesc { get; set; }
        public byte UretTip { get; set; }
        public DateTime Date { get; set; }
        public decimal GrossWeight { get; set; }
        public DateTime Saat { get; set; }
        public string LotNo { get; set; }
        public string SealType { get; set; }
        public string STarih { get; set; }
        public string SSaat { get; set; }
    }
}
