﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.UserSearch
{
    class Unit
    {
        public Unit(int id, string name)
        {
            this.id = id;
            Name = name;
        }

        public int id { get; set; }
        public string Name { get; set; }
    }
}
