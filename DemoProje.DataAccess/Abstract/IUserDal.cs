using DemoProje.Entities.Models;
using Movie.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.DataAccess.Abstract
{
    interface IUserDal : IEntityRepository<User>
    {
    }
}
