using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Prints
{
    public class EfPrintLogsRepository : EfEntityRepositoryBase<PrintLog, Peksan23Context>, IPrintlogsRepository
    {
        public EfPrintLogsRepository(Peksan23Context context) : base(context)
        {
        }
    }
}
