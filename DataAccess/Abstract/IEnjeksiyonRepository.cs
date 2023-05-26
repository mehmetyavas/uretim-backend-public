using Core.DataAccess;
using Entities.Concrete.Uretim;
using Entities.Dtos.Netsis;
using Entities.Dtos.Production.enjeksiyon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IEnjeksiyonRepository:IEntityRepository<RpUretimSeriTakip>
    {
        Task<List<PackageDto>> GetEnjeksiyonPackages(string stokKodu);
    }
}
