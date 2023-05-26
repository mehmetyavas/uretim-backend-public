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
    public class IsemriRecRepository : EfEntityRepositoryBase<Tblisemrirec, Peksan23Context>, IIsemriRecRepository
    {
        public IsemriRecRepository(Peksan23Context context) : base(context)
        {
        }
    }
   
}
