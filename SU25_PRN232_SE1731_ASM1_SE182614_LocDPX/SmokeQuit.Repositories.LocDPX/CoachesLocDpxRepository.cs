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
    public class CoachesLocDpxRepository : GenericRepository<CoachesLocDpx>
    {
        public CoachesLocDpxRepository()
        {
            _context ??= new SU25_PRN232_SE1731_G6_SmokeQuitContext();
        }

        public CoachesLocDpxRepository(SU25_PRN232_SE1731_G6_SmokeQuitContext context) 
        {
            _context = context;
        }

        public async Task<CoachesLocDpx> GetByEmail(string input)
        {
            return await _context.CoachesLocDpxes.FirstOrDefaultAsync(a => a.Email.ToLower().Equals(input));
        }

        public async Task<int> CreateAsync(CoachesLocDpx entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
            
            return entity.CoachesLocDpxid;
        }
    }
}
