using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Giris;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Giris
{
    public class HygieneRepository : EfEntityRepositoryBase<EryMakineTemizlik, Peksan23Context>, IHygieneRepository
    {
        public HygieneRepository(Peksan23Context context) : base(context)
        {
        }

        public async Task<IEnumerable<EryMakineTemizlikSoru>> GetListOfQuestionsAsync(Expression<Func<EryMakineTemizlikSoru, bool>> expression = null)
        {
            return expression == null
                ? await Context.EryMakineTemizlikSorus.ToListAsync()
                : await Context.EryMakineTemizlikSorus.Where(expression).ToListAsync();
        }
    }
}
