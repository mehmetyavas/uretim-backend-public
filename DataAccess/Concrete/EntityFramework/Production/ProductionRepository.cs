using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Uretim;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Production
{
    public class ProductionRepository : EfEntityRepositoryBase<RpUretimSeri, Peksan23Context>, IProductionRepository
    {
        public ProductionRepository(Peksan23Context context) : base(context)
        {
        }


        public async Task<List<RpUretimSeri>> GetListByPagingAsync(string workOrder, int skip, int take)
        {
            return await Context.RpUretimSeris.Where(x => x.IsemriNo == workOrder).OrderByDescending(x=>x.Id).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> ProducedItem(string ciid)
        {
            var result = await Context.RpUretimSeris
                .CountAsync(x =>
                    x.Ciid == ciid &&
                     (x.UretTip == 0 || x.UretTip == 3));
            return result;
        }

        public async Task<string> GetLastSerialNumber(int makId)
        {
            var result = await Context.RpUretimSeris
                .Where(x => x.MakId == makId)
                .OrderByDescending(x => x.Id)
                .Take(10)
                .FirstOrDefaultAsync();



            return result == null ? null : result.SeriNo;
        }
    }
}
