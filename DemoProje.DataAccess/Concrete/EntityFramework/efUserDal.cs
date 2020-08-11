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
    public class efUserDal : efRepositoryBase<User>, IUserDal
    {
        public efUserDal(DbContext dbContext) : base(dbContext)
        {

        }

        public User GetUser(Expression<Func<User, bool>> condition)
        {
            using (var context = new DemoProjeDbContext())
            {
                return context.User
                              .Where(p => p.IsDeleted == false)
                              .FirstOrDefault(condition);
            }
        }
    }
}
