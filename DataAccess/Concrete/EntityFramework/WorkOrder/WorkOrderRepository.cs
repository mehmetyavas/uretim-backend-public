using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Netsis;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete.Uretim;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.WorkOrder
{
    public class WorkOrderRepository : EfEntityRepositoryBase<Tblisemri, Peksan23Context>, IWorkOrderRepository
    {
        public WorkOrderRepository(Peksan23Context context) : base(context)
        {
        }



        //burası sıkıntılı
        public async Task<Dictionary<string, string>> GetIsemriInfo(int MachineId, string IsemriNo)
        {

            //prosedürden gelen veriyi listeye ceviriyor
            var result = await Context
                .Set<IsemriBilgi>()
                .FromSqlInterpolated($"EXEC _ERY_ISEMRI_BILGI1 {MachineId},{IsemriNo}")
                .ToListAsync();
            //Listeyi bir Dictionary'e dönüştürüyor
            var dictResult = result.ToDictionary(x => x.Keys, y => y.Value);

            return dictResult;
        }


    }
}
