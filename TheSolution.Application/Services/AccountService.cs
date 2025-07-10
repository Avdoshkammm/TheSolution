using AutoMapper;
using Microsoft.Extensions.Logging;
using TheSolution.Application.DTO;
using TheSolution.Application.Interfaces;
using TheSolution.Domain.Entities;
using TheSolution.Domain.Interfaces;

namespace TheSolution.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<AccountService> logger;
        public AccountService(IUnitOfWork _unitOfWork, IMapper _mapper, ILogger<AccountService> _logger)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            logger = _logger;
        }

        public async Task Login(UserDTO userdto, string password)
        {
            if(userdto == null)
            {
                logger.LogError("В сервис передан пустой пользователь");
            }
            User registeredUser = mapper.Map<User>(userdto);
            try
            {
                await unitOfWork.Accounts.Login(registeredUser, password);
            }
            catch(Exception ex)
            {
                logger.LogError("Fail at login in service");
            }
        }

        public async Task Register(UserDTO userdto, string password)
        {
            if(userdto == null)
            {
                logger.LogError("В сервис передан пустой пользователь");
            }
            User loginUser = mapper.Map<User>(userdto);
            try
            {
                await unitOfWork.Accounts.Register(loginUser, password);
            }
            catch(Exception ex)
            {
                logger.LogError("Fail at register in service");
            }
        }

        public async Task SignInAsync(UserDTO userdto, bool isPersistent)
        {
            if(userdto == null)
            {
                logger.LogError("В сервис передан пустой пользователь");
                return;
            }
            User siaUser = mapper.Map<User>(userdto);
            try
            {
                await unitOfWork.Accounts.SignInAsyn(siaUser, isPersistent);
            }
            catch(Exception ex)
            {
                logger.LogError("Fail at SignInAsync in service");
            }
        }
    }
}
