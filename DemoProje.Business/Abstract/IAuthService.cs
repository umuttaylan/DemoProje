using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoProje.Business.Abstract
{
    public interface IAuthService
    {
        Task<Identity> Authenticate(string userName, string password);
    }
}
