using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Netsis;
using Entities.Dtos.Netsis;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Netsis
{
    public class SeritraRepository : EfEntityRepositoryBase<Tblseritra, Peksan23Context>, ISeritraRepository
    {
        public SeritraRepository(Peksan23Context context) : base(context)
        {
        }
        public async Task<string> GetLastFisNoByMachineCode(string code)
        {
            var result = await Context.Tblseritras
                    .Where(x => x.Belgeno.Contains($"R{code}0")).MaxAsync(x => x.Belgeno);
            return result;
        }
        public async Task<List<PackageDto>> GetPackageRemainingAmount(string stokKodu)
        {
            var result = await Context.Tblseritras
               .Where(t => t.Depokod == 599 && t.StokKodu == stokKodu)
               .GroupBy(t => new { t.StokKodu, t.SeriNo })
               .Select(g => new PackageDto
               {
                   StokKodu = g.Key.StokKodu,
                   SeriNo = g.Key.SeriNo,
                   Bakiye = g.Sum(t => t.Gckod == "G" ? t.Miktar : -t.Miktar)
               })
               .Where(g => g.Bakiye >= 1)
               .OrderBy(g => g.SeriNo).AsNoTracking()
               .ToListAsync();
            return result;

        }

        public async Task<List<PackageDto>> GetStockRemainingAmount(string stokKodu, int depoKodu, string seriNo)
        {
            var result = await Context.Tblseritras
              .Where(t => t.Depokod == depoKodu && t.StokKodu == stokKodu && t.SeriNo == seriNo)
              .GroupBy(t => new { t.StokKodu, t.SeriNo })
              .Select(g => new PackageDto
              {
                  StokKodu = g.Key.StokKodu,
                  SeriNo = g.Key.SeriNo,
                  Bakiye = g.Sum(t => t.Gckod == "G" ? t.Miktar : -t.Miktar)
              })
              .Where(g => g.Bakiye > 0)
              .OrderBy(g => g.SeriNo).AsNoTracking()
              .ToListAsync();
            return result;
        }



        public async Task<List<PackageDto>> GetStockRemainingAmount(string stokKodu, List<int> depoKodu, string seriNo)
        {
            var result = await Context.Tblseritras
              .Where(t => depoKodu.Contains((int)t.Depokod) && t.StokKodu == stokKodu && t.SeriNo == seriNo)
              .GroupBy(t => new { t.StokKodu, t.SeriNo })
              .Select(g => new PackageDto
              {
                  StokKodu = g.Key.StokKodu,
                  SeriNo = g.Key.SeriNo,
                  Bakiye = g.Sum(t => t.Gckod == "G" ? t.Miktar : -t.Miktar)
              })
              .Where(g => g.Bakiye >= 1)
              .OrderBy(g => g.SeriNo).AsNoTracking()
              .ToListAsync();
            return result;
        }





        public async Task<List<PackageDto>> GetPackageOrBagAmount(string stokKodu)
        {

            var result = await Context.Tblseritras
              .Where(t => t.Depokod == 599 && t.StokKodu == stokKodu)
              .GroupBy(t => new { t.StokKodu, t.SeriNo })
              .Select(g => new
              {
                  g.Key.StokKodu,
                  g.Key.SeriNo,
                  Bakiye = g.Sum(t => t.Gckod == "G" ? t.Miktar : -t.Miktar)
              })
              .Where(g => g.Bakiye >= 1)
              .OrderBy(g => g.SeriNo)
              .ToListAsync();

            List<PackageDto> listResult = new();

            foreach (var item in result)
            {
                listResult.Add(new PackageDto
                {
                    StokKodu = item.StokKodu,
                    SeriNo = item.SeriNo,
                    Bakiye = item.Bakiye
                });
            }


            return listResult;

        }
    }
}
