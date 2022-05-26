
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class StockRepository : EfEntityRepositoryBase<Stock, ProjectDbContext>, IStockRepository
    {
        public StockRepository(ProjectDbContext context) : base(context)
        {
        }

        public Stock GetById(int id)
        {
            Stock stock = Context.Stocks.Where(x => x.Id == id).FirstOrDefault();
            return stock;
        }
    }
}
