using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Stock : IEntity
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public int StorageId { get; set; }
        public Card Card { get; set; }

    }
}
