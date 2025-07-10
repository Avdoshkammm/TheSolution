using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolution.Domain.Entities;

namespace TheSolution.Domain.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(User user, string password);
        Task SignInAsyn(User user, bool ispersistent);
    }
}
