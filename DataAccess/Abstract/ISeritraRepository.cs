using Core.DataAccess;
using Entities.Concrete.Netsis;
using Entities.Dtos.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISeritraRepository : IEntityRepository<Tblseritra>
    {
        Task<string> GetLastFisNoByMachineCode(string code);
        Task<List<PackageDto>> GetPackageRemainingAmount(string stokKodu);
        Task<List<PackageDto>> GetStockRemainingAmount(string stokKodu, int depoKodu, string seriNo);
        Task<List<PackageDto>> GetStockRemainingAmount(string stokKodu, List<int> depoKodu, string seriNo);
        Task<List<PackageDto>> GetPackageOrBagAmount(string stokKodu);
    }
}
