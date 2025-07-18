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

        public async Task<bool> Login(UserDTO userdto, string password)
        {
            if(userdto == null)
            {
                logger.LogError("В сервис передан пустой пользователь");
                return false;
            }
            User loginUser = mapper.Map<User>(userdto);
            try
            {
                return await unitOfWork.Accounts.Login(loginUser, password);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message, ex.Source, "\nError login in service");
                return false;
            }
        }

        public async Task Register(UserDTO userdto, string password)
        {
            if(userdto == null)
            {
                logger.LogError("Null user in service(Register)");
            }
            User loginUser = mapper.Map<User>(userdto);
            try
            {
                await unitOfWork.Accounts.Register(loginUser, password);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message, ex.Source, "\nFail at register in service");
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
                logger.LogError(ex.Message, ex.Source, "\nFail at SignInAsync in service");
            }
        }
    }
}
