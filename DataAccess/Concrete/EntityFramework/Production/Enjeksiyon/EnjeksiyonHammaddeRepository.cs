using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Production.Enjeksiyon
{
    public class EnjeksiyonHammaddeRepository : EfEntityRepositoryBase<EryHammaddeVarilseri, Peksan23Context>
        , IEnjeksiyonHammaddeRepository
    {
        public EnjeksiyonHammaddeRepository(Peksan23Context context) : base(context)
        {
        }
    }
}
