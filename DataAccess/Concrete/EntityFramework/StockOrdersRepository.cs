﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class StockOrdersRepository : EfEntityRepositoryBase<StockOrders, ProjectDbContext>, IStockOrdersRepository
    {
        public StockOrdersRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
