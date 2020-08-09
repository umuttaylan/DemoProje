using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Models;
using Microsoft.EntityFrameworkCore;
using DemoProje.Core.DataAccess.EntityFramework;

namespace DemoProje.DataAccess.Concrete.EntityFramework
{
    public class efVehicleDal : efRepositoryBase<Vehicle>, IVehicleDal
    {
        public efVehicleDal(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
