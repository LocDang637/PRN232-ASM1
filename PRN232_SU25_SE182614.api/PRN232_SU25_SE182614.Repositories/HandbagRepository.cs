using PRN232_SU25_SE182614.Repositories.Basic;
using PRN232_SU25_SE182614.Repositories.DBContext;
using PRN232_SU25_SE182614.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_SE182614.Repositories
{
    public class HandbagRepository : GenericRepository<Handbag>
    {
        public HandbagRepository() { }
        public HandbagRepository(HandbagDbContext context)
        {
            _context = context;
        }
    }
}
