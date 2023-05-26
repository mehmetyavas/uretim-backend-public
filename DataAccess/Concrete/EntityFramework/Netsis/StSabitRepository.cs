using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Netsis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Netsis
{
    public class StSabitRepository : EfEntityRepositoryBase<Tblstsabit, Peksan23Context>, IStSabitRepository
    {
        public StSabitRepository(Peksan23Context context) : base(context)
        {
        }

        public async Task<List<string>> GetStockCodeListAsync(string kod1)
        {
            return await Context.Tblstsabits.Where(x => x.Kod1 == kod1).Select(x => x.StokKodu).AsNoTracking().ToListAsync();
        }
    }
}
