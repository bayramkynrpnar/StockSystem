﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Card : IEntity
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string SerialNo { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public double Price { get; set; }
    }
}
