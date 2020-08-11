using DemoProje.Entities.Models;
using DemoProje.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace DemoProje.DataAccess.Abstract
{
    public interface IMaintenanceHistoryDal : IEntityRepository<MaintenanceHistory>
    {
        MaintenanceHistory GetMaintenanceHistory(Expression<Func<MaintenanceHistory, bool>> condition);

    }
}
