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
    public class efStatusDal : efRepositoryBase<Status>, IStatusDal
    {
        public efStatusDal(DbContext dbContext) : base(dbContext)
        {

        }

        public Status GetStatus(Expression<Func<Status, bool>> condition)
        {
            var result = new Status();
            using (var context = new DemoProjeDbContext())
            {
                result = context.Status
                              .Where(p => p.IsDeleted == false)
                              .FirstOrDefault(condition);
            }

            return result;
        }
    }
}
