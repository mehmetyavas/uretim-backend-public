using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Uretim;

public partial class RpIsemriBilgi : IEntity
{
    public int Id { get; set; }

    public string Isemrino { get; set; }

    public string Serino { get; set; }

    public decimal BirimAgirlik { get; set; }

    public decimal Maxkg { get; set; }

    public decimal Minkg { get; set; }

    public decimal Maxad { get; set; }

    public decimal Minad { get; set; }

    public DateTime Tarih { get; set; }
}
