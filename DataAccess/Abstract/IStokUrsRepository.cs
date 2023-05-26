using Core.DataAccess;
using Entities.Concrete.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStokUrsRepository : IEntityRepository<Tblstokur>
    {
        Task<string> GetLastFisNoByMachineCode(string code);
    }
}
