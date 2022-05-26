﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Storage : IEntity
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public bool Status{ get; set; }
    }
}
