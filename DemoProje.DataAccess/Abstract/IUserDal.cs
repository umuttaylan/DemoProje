using DemoProje.Entities.Models;
using DemoProje.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProje.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
