using Progetto_BE_S7.Data;

namespace Progetto_BE_S7.Services
{
    public class EventoServices
    {
        private readonly ApplicationDbContext _context;
        public EventoServices(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
