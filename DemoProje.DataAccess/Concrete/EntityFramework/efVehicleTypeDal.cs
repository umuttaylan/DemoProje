using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Models;
using Microsoft.EntityFrameworkCore;
using DemoProje.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;

namespace DemoProje.DataAccess.Concrete.EntityFramework
{
    public class efVehicleTypeDal : efRepositoryBase<VehicleType>, IVehicleTypeDal
    {
        public efVehicleTypeDal(DbContext dbContext) : base(dbContext)
        {

        }

        public VehicleType GetVehicleType(Expression<Func<VehicleType, bool>> condition)
        {
            var result = new VehicleType();
            using (var context = new DemoProjeDbContext())
            {
                result = context.VehicleType
                              .Where(p => p.IsDeleted != true)
                              .FirstOrDefault(condition);
            }

            return result;
        }
    }
}
