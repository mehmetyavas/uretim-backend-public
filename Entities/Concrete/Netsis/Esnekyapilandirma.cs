using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Netsis;

public partial class Esnekyapilandirma : IEntity
{
    public string StokKodu { get; set; }

    public string Yapkod { get; set; }

    public string Ozkod { get; set; }

    public string Ozacik { get; set; }

    public string Degerkod { get; set; }

    public string Degeracik { get; set; }

    public string EngDesc { get; set; }
}
