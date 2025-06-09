using SmokeQuit.Repositories.LocDPX;
using SmokeQuit.Repositories.LocDPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeQuit.Services.LocDPX
{
    public class SystemUserAccountService
    {
        private readonly SystemUserAccountRepository _repository;
        public SystemUserAccountService()
        {
            _repository ??= new SystemUserAccountRepository();
        }
        
        public SystemUserAccountService(SystemUserAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<SystemUserAccount> GetAccount(string userName, string password)
        {
            return await _repository.GetAccount(userName, password);
        }

        public async Task<List<SystemUserAccount>> GetAccounts()
        {
            return await _repository.GetAllAsync();
        }
    }
}
