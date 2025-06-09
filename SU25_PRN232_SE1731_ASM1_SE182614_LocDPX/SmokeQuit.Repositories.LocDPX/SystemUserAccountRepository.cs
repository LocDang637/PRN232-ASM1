using Microsoft.EntityFrameworkCore;
using SmokeQuit.Repositories.LocDPX.Basic;
using SmokeQuit.Repositories.LocDPX.DBContext;
using SmokeQuit.Repositories.LocDPX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeQuit.Repositories.LocDPX
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository()
        {
    
        }
        public SystemUserAccountRepository(SU25_PRN232_SE1731_G6_SmokeQuitContext context) 
        {
            _context = context;
        }
    
        public async Task<SystemUserAccount> GetAccount(string userName, string password)
        {
            return await _context.SystemUserAccounts.FirstOrDefaultAsync(x => x.UserName.ToLower() == userName && x.Password == password && x.IsActive == true);
        }
    }
}
