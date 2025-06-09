using SmokeQuit.Repositories.DuongLNT.Basic;
using SmokeQuit.Repositories.DuongLNT.DBContext;
using SmokeQuit.Repositories.DuongLNT.Models;

namespace SmokeQuit.Repositories.DuongLNT
{
	public class QuitPlansAnhDtnDuongLNTRepository : GenericRepository<QuitPlansAnhDtn>
	{
		public QuitPlansAnhDtnDuongLNTRepository() { }

		public QuitPlansAnhDtnDuongLNTRepository(SU25_PRN232_SE1731_G6_SmokeQuitContext context) => _context = context;
	}
}
