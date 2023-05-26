using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Uretim;
using Entities.Dtos.Netsis;
using Entities.Dtos.Production.enjeksiyon;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Production.Enjeksiyon
{
    public class EnjeksiyonRepository : EfEntityRepositoryBase<RpUretimSeriTakip, Peksan23Context>, IEnjeksiyonRepository
    {
        public EnjeksiyonRepository(Peksan23Context context) : base(context)
        {
        }



        public async Task<List<PackageDto>> GetEnjeksiyonPackages(string stokKodu)
        {
            var result = await Context.RpUretimSeriTakips
               .Where(t => Context.RpUretimSeris
                   .Where(u => u.Uretildi == false && u.UretTip != 1)
                   .Select(u => u.Id)
                   .Contains(t.UretId)
               && t.VsStokKodu == stokKodu)
               .GroupBy(t => new { t.VsStokKodu, t.VsSeriNo })
               .Select(g => new PackageDto
               {
                   StokKodu = g.Key.VsStokKodu,
                   SeriNo = g.Key.VsSeriNo,
                   Bakiye = g.Sum(t => t.Harcanan)
               })
               .ToListAsync();
            return result;
        }


    }
}
