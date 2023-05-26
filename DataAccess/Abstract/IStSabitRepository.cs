﻿using Core.DataAccess;
using Core.Entities;
using Entities.Concrete.Netsis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStSabitRepository:IEntityRepository<Tblstsabit>
    {
        Task<List<string>> GetStockCodeListAsync(string kod1);
    }
}
