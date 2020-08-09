﻿using DemoProje.DataAccess.Abstract;
using DemoProje.Entities.Models;
using Microsoft.EntityFrameworkCore;
using DemoProje.Core.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.DataAccess.Concrete.EntityFramework
{
    public class efActionTypeDal : efRepositoryBase<ActionType>, IActionTypeDal
    {
        public efActionTypeDal(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
