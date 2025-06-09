using SmokeQuit.Repositories.DuongLNT;
using SmokeQuit.Repositories.DuongLNT.Models;

namespace SmokeQuit.Services.DuongLNT
{
	public class SystemUserAccountService
	{
		private readonly SystemUserAccountRepository _repository;
		public SystemUserAccountService() => _repository ??= new SystemUserAccountRepository();

		public async Task<SystemUserAccount> GetUserAccount(string username, string password)
		{
			return await _repository.GetUserAccount(username, password);
		}

		public async Task<List<SystemUserAccount>> GetAllUserAsync()
		{
			return await _repository.GetAllAsync();
		}
	}
}
