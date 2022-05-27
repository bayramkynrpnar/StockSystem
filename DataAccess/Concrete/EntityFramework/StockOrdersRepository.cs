
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class StockOrdersRepository : EfEntityRepositoryBase<StockOrders, ProjectDbContext>, IStockOrdersRepository
    {
        public StockOrdersRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<Report>> GetReportDto()
        {
            var list = await (from lng in Context.Cards
                        join trs in Context.Stocks on lng.Id equals trs.CardId
                        join stc in Context.StockOrderses on lng.Id equals stc.CardId
                        select new Report()
                        {
                            Quantity = trs.Quantity,
                            ProductName = lng.Name,
                            Date = stc.OrderDate,
                            MovementName = stc.OrderType.ToString(),
                            OrderBy = stc.OrderBy
                        }).ToListAsync();
            return list;

        }
    }
}
