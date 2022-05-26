
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class StorageRepository : EfEntityRepositoryBase<Storage, ProjectDbContext>, IStorageRepository
    {
        public StorageRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
