using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Giris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Giris
{
    public class MachineRepository : EfEntityRepositoryBase<AncTblmachine, Peksan23Context>, IMachineRepository
    {
        public MachineRepository(Peksan23Context context) : base(context)
        {
        }
    }
}
