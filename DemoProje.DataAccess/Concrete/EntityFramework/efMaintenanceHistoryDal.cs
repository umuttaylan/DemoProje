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
    public class efMaintenanceHistoryDal : efRepositoryBase<MaintenanceHistory>, IMaintenanceHistoryDal
    {
        public efMaintenanceHistoryDal(DbContext dbContext): base(dbContext)
        {

        }

        public MaintenanceHistory GetMaintenanceHistory(Expression<Func<MaintenanceHistory, bool>> condition)
        {
            var result = new MaintenanceHistory();
            using (var context = new DemoProjeDbContext())
            {
                result = context.MaintenanceHistory
                              .Where(p => p.IsDeleted != true)
                              .FirstOrDefault(condition);
            }

            return result;
        }
    }
}
