using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete.Giris;

public partial class AncTblstaff : IEntity
{
    public int Id { get; set; }

    public string StaffCode { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Gozluk { get; set; }

    public string Telefon { get; set; }
}
