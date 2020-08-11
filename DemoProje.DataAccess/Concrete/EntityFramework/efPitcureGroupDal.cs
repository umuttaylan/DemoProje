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
    public class efPitcureGroupDal : efRepositoryBase<PictureGroup>, IPictureGroupDal
    {
        public efPitcureGroupDal(DbContext dbContext) : base(dbContext)
        {

        }

        public PictureGroup GetPictureGroup(Expression<Func<PictureGroup, bool>> condition)
        {
            var result = new PictureGroup();
            using (var context = new DemoProjeDbContext())
            {
                result = context.PictureGroup
                              .Where(p => p.IsDeleted == false)
                              .FirstOrDefault(condition);
            }

            return result;
        }
    }
}
