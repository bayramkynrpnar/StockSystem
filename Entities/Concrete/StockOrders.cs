using Core.Entities;
using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class StockOrders : IEntity
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderBy { get; set; }
    }
}
