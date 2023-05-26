using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Uretim;

public partial class RpMontajHmBilgi1 : IEntity
{
    public string Isemrino { get; set; }

    public string HamKodu { get; set; }

    public string Hamyapkod { get; set; }

    public string UrunTip { get; set; }

    public string Degeracik { get; set; }
}
