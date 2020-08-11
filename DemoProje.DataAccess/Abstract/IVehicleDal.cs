using DemoProje.Entities.Models;
using DemoProje.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DemoProje.DataAccess.Abstract
{
    public interface IVehicleDal : IEntityRepository<Vehicle>
    {
        Vehicle GetVehicle(Expression<Func<Vehicle, bool>> condition);
    }
}
