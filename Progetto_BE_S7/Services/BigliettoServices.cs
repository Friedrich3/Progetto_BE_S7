using Progetto_BE_S7.Data;

namespace Progetto_BE_S7.Services
{
    public class BigliettoServices
    {
        private readonly ApplicationDbContext _context;
        public BigliettoServices (ApplicationDbContext context)
        {
            _context = context;
        }
        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
