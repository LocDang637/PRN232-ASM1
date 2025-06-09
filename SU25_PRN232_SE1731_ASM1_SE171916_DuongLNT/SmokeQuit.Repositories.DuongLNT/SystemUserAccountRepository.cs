using Microsoft.EntityFrameworkCore;
using SmokeQuit.Repositories.DuongLNT.Basic;
using SmokeQuit.Repositories.DuongLNT.DBContext;
using SmokeQuit.Repositories.DuongLNT.Models;

namespace SmokeQuit.Repositories.DuongLNT
{
	public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
	{
		public SystemUserAccountRepository() { }

		public SystemUserAccountRepository(SU25_PRN232_SE1731_G6_SmokeQuitContext context) => _context = context;

		public async Task<SystemUserAccount> GetUserAccount (string userName, string password)
		{
			//return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Email == userName && u.Password == password && u.IsActive == true);
			//return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.Phone == userName && u.Password == password);
			return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
			//return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == userName && u.Password == password);
		}


	}
}
