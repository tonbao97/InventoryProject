﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Department: BasicAttr
    {
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}
