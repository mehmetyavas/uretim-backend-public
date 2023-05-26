using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Giris;

public partial class EryMakineTemizlikSoru : IEntity
{
    public int Id { get; set; }

    public int CalismaYeri { get; set; }

    public string Soru { get; set; }

    public int? SoruId { get; set; }
}
