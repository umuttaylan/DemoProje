﻿using DemoProje.DataAccess.Abstract;
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
    public class efMaintenanceDal : efRepositoryBase<Maintenance>, IMaintenanceDal
    {
        public efMaintenanceDal(DbContext dbContext): base(dbContext)
        {

        }

        public Maintenance GetMaintenance(Expression<Func<Maintenance, bool>> condition)
        {
            var result = new Maintenance();
            using (var context = new DemoProjeDbContext())
            {
                result = context.Maintenance
                              .Where(p => p.IsDeleted == false)
                              .FirstOrDefault(condition);
            }

            return result;
        }
    }
}
