using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class Report : IDto
    {
        public string ProductName { get; set; }
        public string MovementName { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public string OrderBy { get; set; }
    }
}
