using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Giris;

public partial class AncTblmachine : IEntity
{
    public int Id { get; set; }

    public string MachineCode { get; set; }

    public string Description1 { get; set; }

    public string Description2 { get; set; }

    public string ProductionArea { get; set; }

    public string Code { get; set; }
}
