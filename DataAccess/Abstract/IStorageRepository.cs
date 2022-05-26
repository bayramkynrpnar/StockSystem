
using System;
using Core.DataAccess;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IStorageRepository : IEntityRepository<Storage>
    {
        Storage GetById(int id);
    }
}