using Microsoft.EntityFrameworkCore;
using SmokeQuit.Repositories.LocDPX.Basic;
using SmokeQuit.Repositories.LocDPX.DBContext;
using SmokeQuit.Repositories.LocDPX.Models;
using SmokeQuit.Repositories.LocDPX.ModelExtensions;

namespace SmokeQuit.Repositories.LocDPX
{
    public class ChatsLocDpxRepository : GenericRepository<ChatsLocDpx>
    {
        public ChatsLocDpxRepository()
        {
            _context ??= new SU25_PRN232_SE1731_G6_SmokeQuitContext();
        }

        public ChatsLocDpxRepository(SU25_PRN232_SE1731_G6_SmokeQuitContext context)
        {
            _context = context;
        }

        // Override GetAllAsync with includes
        public async Task<List<ChatsLocDpx>> GetAllAsync()
        {
            return await _context.ChatsLocDpxes
                .Include(x => x.Coach)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        // Override GetByIdAsync with includes
        public async Task<ChatsLocDpx> GetByIdAsync(int id)
        {
            return await _context.ChatsLocDpxes
                .Include(x => x.Coach)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ChatsLocDpxid == id);
        }

        // Search with multiple criteria
        public async Task<List<ChatsLocDpx>> SearchAsync(string message, string messageType, string sentBy, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.ChatsLocDpxes
                .Include(x => x.Coach)
                .Include(x => x.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(message))
                query = query.Where(x => x.Message.Contains(message));

            if (!string.IsNullOrEmpty(messageType))
                query = query.Where(x => x.MessageType.Contains(messageType));

            if (!string.IsNullOrEmpty(sentBy))
                query = query.Where(x => x.SentBy.Contains(sentBy));

            if (startDate.HasValue)
                query = query.Where(x => x.CreatedAt >= startDate);

            if (endDate.HasValue)
                query = query.Where(x => x.CreatedAt <= endDate);

            return await query.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        // Search with pagination
        public async Task<PaginationResult<List<ChatsLocDpx>>> SearchWithPagingAsync(string message, string messageType, string sentBy, DateTime? startDate, DateTime? endDate, int currentPage, int pageSize)
        {
            var chats = await SearchAsync(message, messageType, sentBy, startDate, endDate);

            var totalItems = chats.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            chats = chats.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<List<ChatsLocDpx>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = chats
            };
        }

        // Get all with pagination
        public async Task<PaginationResult<List<ChatsLocDpx>>> GetAllWithPagingAsync(int currentPage, int pageSize)
        {
            var chats = await GetAllAsync();

            var totalItems = chats.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            chats = chats.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<List<ChatsLocDpx>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = chats
            };
        }

        // Get chats by user
        public async Task<List<ChatsLocDpx>> GetChatsByUserAsync(int userId)
        {
            return await _context.ChatsLocDpxes
                .Include(x => x.Coach)
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        // Get chats by coach
        public async Task<List<ChatsLocDpx>> GetChatsByCoachAsync(int coachId)
        {
            return await _context.ChatsLocDpxes
                .Include(x => x.Coach)
                .Include(x => x.User)
                .Where(x => x.CoachId == coachId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}