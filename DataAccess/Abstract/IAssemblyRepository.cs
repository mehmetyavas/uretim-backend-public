using Core.DataAccess;
using Entities.Concrete.Uretim;
using Entities.Dtos.Netsis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAssemblyRepository : IEntityRepository<RpUretimSeriTakipMontaj>
    {
        Task<List<PackageDto>> GetAssemblyPackage(string stokKodu);
    }
}
