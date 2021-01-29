﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        IQueryable<Discount> Get();
    }
}