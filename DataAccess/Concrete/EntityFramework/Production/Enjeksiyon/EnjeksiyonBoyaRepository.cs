using Core.DataAccess;
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
    public class EnjeksiyonBoyaRepository : EfEntityRepositoryBase<EryBoyaSeri, Peksan23Context>, IEnjeksiyonBoyaRepository
    {
        public EnjeksiyonBoyaRepository(Peksan23Context context) : base(context)
        {
        }
    }
}
