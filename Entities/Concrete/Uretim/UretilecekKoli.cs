using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Uretim;

public partial class UretilecekKoli:IEntity
{
    public float? Value { get; set; }

    public int Ciid { get; set; }

    public int Did { get; set; }
}
