using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Domain;
using Domain.Aggregates.Cinemas;
using System.Data.Entity;

namespace Infrastructure.Repositories
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly DatabaseContext _context;

        public CinemaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Cinema> GetCinemaById(int cinemaId)
        {
            return await _context.Cinemas
                .Include(x => x.Screens)
                .FirstOrDefaultAsync(x => x.Id == cinemaId);
        }
    }
}
