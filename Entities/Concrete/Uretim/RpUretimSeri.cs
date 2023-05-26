using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Uretim;

public partial class RpUretimSeri : IEntity
{
    public int Id { get; set; }

    public string SeriNo { get; set; }

    public int PersonelId { get; set; }

    public string IsemriNo { get; set; }

    public int MakId { get; set; }

    public decimal BAgirlik { get; set; }

    public decimal Dara { get; set; }

    public decimal Net { get; set; }

    public decimal Adet { get; set; }

    public string StokKodu { get; set; }

    public string YapKod { get; set; }

    public bool Uretildi { get; set; }

    public byte UretTip { get; set; }

    public DateTime Created { get; set; }

    public string SipNo { get; set; }

    public string Ciid { get; set; }

    public decimal? Brut { get; set; }

    public string LotNo { get; set; }

    public string Vardiya { get; set; }

    public DateTime? Tarih { get; set; }

    public string SuskNo { get; set; }

    public string HataliLot { get; set; }

    public bool? SuskHata { get; set; }

    public int? Siralama { get; set; }

    public int? UretimNo { get; set; }

    public string SuskHataMsg { get; set; }

    public int? EskiId { get; set; }

    public string Terazi { get; set; }
}
