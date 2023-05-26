using Core.DataAccess;
using Entities.Concrete.Giris;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace DataAccess.Abstract
{
    public interface IHygieneRepository:IEntityRepository<EryMakineTemizlik>
    {
        Task<IEnumerable<EryMakineTemizlikSoru>> GetListOfQuestionsAsync(Expression<Func<EryMakineTemizlikSoru, bool>> expression = null);
    }
}