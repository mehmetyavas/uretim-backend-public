using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Uretim;
using Entities.Dtos.WorkOrder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.WorkOrder
{
    public class AssemblyRawMRepository : EfEntityRepositoryBase<RpMontajHmBilgi1, Peksan23Context>, IAssemblyRawMRepository
    {

        public AssemblyRawMRepository(Peksan23Context context) : base(context)
        {
        }


        public async Task<AssemblyInfoDTO> GetAssemblyRawMateriaInfo(string isemriNo)
        {
            //hammadde gövde bilgisi
            var rawMaterialBody = await Context.RpMontajHmBilgi1s.Where(x =>
                                    x.Isemrino == isemriNo &&
                                    x.UrunTip != "02").SingleOrDefaultAsync();
            //hammadde üst bilgisi
            var rawMaterialTop = await Context.RpMontajHmBilgi1s.Where(x =>
                                    x.Isemrino == isemriNo &&
                                    x.UrunTip == "02").SingleOrDefaultAsync();



            if (rawMaterialBody == null)
            {
                return new AssemblyInfoDTO
                {
                    RawMaterialBody = null,
                    RawMaterialTop = null,
                };
            }
            return rawMaterialTop != null
                ? new AssemblyInfoDTO
                {
                    RawMaterialBody = $"{rawMaterialBody!.HamKodu} / {rawMaterialBody.Degeracik}",
                    RawMaterialTop = $"{rawMaterialTop!.HamKodu} / {rawMaterialTop.Degeracik}",
                }
                : new AssemblyInfoDTO
                {
                    RawMaterialBody = $"{rawMaterialBody!.HamKodu} / {rawMaterialBody.Degeracik}",
                    RawMaterialTop = null
                };

        }

    }
}
