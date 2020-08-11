using DemoProje.Entities.Models;
using DemoProje.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DemoProje.DataAccess.Abstract
{
    public interface IMaintenanceDal : IEntityRepository<Maintenance>
    {
        Maintenance GetMaintenance(Expression<Func<Maintenance, bool>> condition);
        List<Maintenance> GetMaintenanceList(Expression<Func<Maintenance, bool>> condition = null);
    }
}
