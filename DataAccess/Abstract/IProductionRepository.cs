using Core.DataAccess;
using Entities.Concrete.Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductionRepository : IEntityRepository<RpUretimSeri>
    {
        Task<List<RpUretimSeri>> GetListByPagingAsync(string workOrder, int skip, int take);
        Task<int> ProducedItem(string ciid);
        Task<string> GetLastSerialNumber(int makId);
    }
}
