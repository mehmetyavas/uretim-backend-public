using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Uretim;
using Entities.Dtos.Netsis;
using Entities.Dtos.Production.enjeksiyon;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Production.Montaj
{
    public class AssemblyRepository : EfEntityRepositoryBase<RpUretimSeriTakipMontaj, Peksan23Context>, IAssemblyRepository
    {
        public AssemblyRepository(Peksan23Context context) : base(context)
        {
        }


        public async Task<List<PackageDto>> GetAssemblyPackage(string stokKodu)
        {
            var result = await Context.RpUretimSeriTakipMontajs
               .Where(t => Context.RpUretimSeris
                   .Where(u => u.Uretildi == false && u.UretTip != 1)
                   .Select(u => u.Id)
                   .Contains(t.UretId)
               && t.StokKodu == stokKodu)
               .GroupBy(t => new { t.StokKodu, t.SeriNo })
               .Select(g => new PackageDto
               {
                   StokKodu = g.Key.StokKodu,
                   SeriNo = g.Key.SeriNo,
                   Bakiye = g.Sum(t => t.Harcanan)
               })
               .ToListAsync();
            return result;

        }
    }
}
