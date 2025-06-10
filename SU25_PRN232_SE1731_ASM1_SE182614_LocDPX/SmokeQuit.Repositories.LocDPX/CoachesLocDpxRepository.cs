using Microsoft.EntityFrameworkCore;
using SmokeQuit.Repositories.LocDPX.Basic;
using SmokeQuit.Repositories.LocDPX.DBContext;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Repositories.LocDPX.ModelExtensions;

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

        // Override GetAllAsync
        public async Task<List<CoachesLocDpx>> GetAllAsync()
        {
            return await _context.CoachesLocDpxes
                .OrderBy(x => x.FullName)
                .ToListAsync();
        }

        // Get by email
        public async Task<CoachesLocDpx> GetByEmailAsync(string email)
        {
            return await _context.CoachesLocDpxes
                .FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower());
        }

        // Search coaches
        public async Task<List<CoachesLocDpx>> SearchAsync(string fullName, string email)
        {
            var query = _context.CoachesLocDpxes.AsQueryable();

            if (!string.IsNullOrEmpty(fullName))
                query = query.Where(x => x.FullName.Contains(fullName));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.Contains(email));

            return await query.OrderBy(x => x.FullName).ToListAsync();
        }

        // Search with pagination
        public async Task<PaginationResult<List<CoachesLocDpx>>> SearchWithPagingAsync(string fullName, string email, int currentPage, int pageSize)
        {
            var coaches = await SearchAsync(fullName, email);

            var totalItems = coaches.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            coaches = coaches.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<List<CoachesLocDpx>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = coaches
            };
        }

        // Get all with pagination
        public async Task<PaginationResult<List<CoachesLocDpx>>> GetAllWithPagingAsync(int currentPage, int pageSize)
        {
            var coaches = await GetAllAsync();

            var totalItems = coaches.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            coaches = coaches.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<List<CoachesLocDpx>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = coaches
            };
        }

        // Create with return ID
        public async Task<int> CreateAsync(CoachesLocDpx entity)
        {
            entity.CreatedAt = DateTime.Now;
            _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity.CoachesLocDpxid;
        }
    }
}