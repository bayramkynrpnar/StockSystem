
using System;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IStockRepository : IEntityRepository<Stock>
    {
        Stock GetById(int id);
    }
}