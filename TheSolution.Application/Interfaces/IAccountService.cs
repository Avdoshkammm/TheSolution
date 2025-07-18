using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolution.Application.DTO;

namespace TheSolution.Application.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Login(UserDTO userDTO, string password);
        Task Register(UserDTO userDTO, string password);
        Task SignInAsync(UserDTO userdto, bool isPersistent);
    }
}
