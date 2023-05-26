using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Uretim;

public partial class MontajHammaddeSeri : IEntity
{
    public int Id { get; set; }

    public string WorkOrder { get; set; }

    public string StockCode { get; set; }

    public string YapKod { get; set; }

    public string SerialNo { get; set; }

    public decimal Quantity { get; set; }

    public decimal Spent { get; set; }

    public int UretId { get; set; }

    public string ProductType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
