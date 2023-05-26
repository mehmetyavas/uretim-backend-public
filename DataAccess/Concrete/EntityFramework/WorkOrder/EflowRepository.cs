using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.WorkOrder
{
    public class EflowRepository : EfEntityRepositoryBase<UretilecekKoli, EflowContext>, IEflowRepository
    {
        public EflowRepository(EflowContext context) : base(context)
        {
        }
    }
}
