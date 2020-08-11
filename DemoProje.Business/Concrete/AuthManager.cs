using DemoProje.Business.Abstract;
using DemoProje.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProje.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        List<Identity> _identities = new List<Identity>{
            new Identity{Id=1,FirstName="Test",LastName="User",UserName="test",Password="test"}
        };

        public async Task<Identity> Authenticate(string userName, string password)
        {
            var user = await Task.Run(() => _identities.FirstOrDefault(x => x.UserName == userName && x.Password == password));

            if (user == null)
            {
                return null;
            }

            user.Password = null;
            return user;
        }
    }
}
