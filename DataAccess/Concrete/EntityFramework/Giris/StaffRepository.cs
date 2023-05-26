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
    public class StaffRepository : EfEntityRepositoryBase<AncTblstaff, Peksan23Context>, IStaffRepository
    {
        public StaffRepository(Peksan23Context context) : base(context)
        {
        }
    }

}
