using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Models;
using Microsoft.EntityFrameworkCore;
using DemoProje.Core.DataAccess.EntityFramework;
using System.Linq.Expressions;
using System;
using System.Linq;

namespace DemoProje.DataAccess.Concrete.EntityFramework
{
    public class efVehicleDal : efRepositoryBase<Vehicle>, IVehicleDal
    {
        public efVehicleDal(DbContext dbContext) : base(dbContext)
        {

        }

        public Vehicle GetVehicle(Expression<Func<Vehicle, bool>> condition)
        {
            var result = new Vehicle();
            using (var context = new DemoProjeDbContext())
            {
                result = context.Vehicle
                              .Where(p => p.IsDeleted != true)
                              .FirstOrDefault(condition);
            }

            return result;
        }
    }
}
