using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Netsis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Netsis
{
    public class StokUrsRepository : EfEntityRepositoryBase<Tblstokur, Peksan23Context>, IStokUrsRepository
    {
        public StokUrsRepository(Peksan23Context context) : base(context)
        {
        }

        public async Task<string> GetLastFisNoByMachineCode(string code)
        {
            var result = await Context.Tblstokurs.AsNoTracking()
                    .Where(x => x.UretsonFisno.Contains($"R{code}0")).MaxAsync(x => x.UretsonFisno);
            return result;
        }
    }
}
