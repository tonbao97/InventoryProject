﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class MovementLog: BaseEntity
    {
        public DateTime Time { get; set; }
        public int ItemID { get; set; }
        public string GiverType { get; set; }
        public int? GiverID { get; set; }
        public string ReceiverType { get; set; }
        public int? ReceiverID { get; set; }

        public MovementLog()
        {
            this.Time = DateTime.Now;
        }

        public virtual Item Item { get; set; }
        [ForeignKey("GiverID")]
        public virtual Staff Giver { get; set; }
        [ForeignKey("ReceiverID")]
        public virtual Staff Receiver { get; set; }


    }
}
