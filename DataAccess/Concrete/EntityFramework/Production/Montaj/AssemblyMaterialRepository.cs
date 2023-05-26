using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Uretim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Production.Montaj
{
    public class AssemblyMaterialRepository : EfEntityRepositoryBase<MontajHammaddeSeri, Peksan23Context>, IAssemblyMaterialRepository
    {
        public AssemblyMaterialRepository(Peksan23Context context) : base(context)
        {
        }
    }
}
