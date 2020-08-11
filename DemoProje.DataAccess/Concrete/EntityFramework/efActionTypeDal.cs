using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Models;
using Microsoft.EntityFrameworkCore;
using DemoProje.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DemoProje.DataAccess.Concrete.EntityFramework
{
    public class efActionTypeDal : efRepositoryBase<ActionType>, IActionTypeDal
    {
        public efActionTypeDal(DbContext dbContext) : base(dbContext)
        {
        }

        public ActionType GetActionType(Expression<Func<ActionType, bool>> condition)
        {
            var result = new ActionType();
            using (var context = new DemoProjeDbContext())
            {
                result = context.ActionType
                              .Where(p => p.IsDeleted != true)
                              .FirstOrDefault(condition);
            }

            return result;
        }
    }
}
