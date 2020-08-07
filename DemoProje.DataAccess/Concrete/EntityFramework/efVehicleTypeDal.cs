using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Movie.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.DataAccess.Concrete.EntityFramework
{
    public class efVehicleTypeDal : efRepositoryBase<VehicleType>, IVehicleTypeDal
    {
        public efVehicleTypeDal(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
