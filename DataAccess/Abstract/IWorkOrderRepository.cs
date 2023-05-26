using Core.DataAccess;
using Entities.Concrete.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IWorkOrderRepository : IEntityRepository<Tblisemri>
    {
        public Task<Dictionary<string, string>> GetIsemriInfo(int MachineId, string IsemriNo);

    }
}
