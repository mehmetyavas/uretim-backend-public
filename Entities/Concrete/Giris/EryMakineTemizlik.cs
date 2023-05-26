using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Giris;

public partial class EryMakineTemizlik : IEntity
{
    public int Id { get; set; }

    public int Query { get; set; }

    public int Staff { get; set; }

    public int Machine { get; set; }

    public int Clear { get; set; }

    public DateTime? Opentime { get; set; }

    public int? Question { get; set; }

    public string Desc { get; set; }
}
