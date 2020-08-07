using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Movie.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.DataAccess.Concrete.EntityFramework
{
    public class efUserDal : efRepositoryBase<User>, IUserDal
    {
        public efUserDal(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
