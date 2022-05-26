
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class CardRepository : EfEntityRepositoryBase<Card, ProjectDbContext>, ICardRepository
    {
        public CardRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
