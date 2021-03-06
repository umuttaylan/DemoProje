﻿using DemoProje.Entities.Models;
using DemoProje.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DemoProje.DataAccess.Abstract
{
    public interface IVehicleTypeDal : IEntityRepository<VehicleType>
    {
        VehicleType GetVehicleType(Expression<Func<VehicleType, bool>> condition);
    }
}
