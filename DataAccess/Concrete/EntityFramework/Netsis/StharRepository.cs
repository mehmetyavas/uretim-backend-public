using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Netsis
{
    public class StharRepository : EfEntityRepositoryBase<Tblsthar, Peksan23Context>, IStharRepository
    {
        public StharRepository(Peksan23Context context) : base(context)
        {
        }
    }
}
