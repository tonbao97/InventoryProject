﻿using Data.Infrastructure;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class EquipmentTypeRepository : RepositoryBase<EquipmentType>, IEquipmentTypeRepository
    {
        public EquipmentTypeRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IEquipmentTypeRepository : IRepository<EquipmentType>
    {

    }
}
