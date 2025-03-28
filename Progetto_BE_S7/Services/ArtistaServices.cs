using Progetto_BE_S7.Data;

namespace Progetto_BE_S7.Services
{
    public class ArtistaServices
    {
        private readonly ApplicationDbContext _context;
        public ArtistaServices(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
