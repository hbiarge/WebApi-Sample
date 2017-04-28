using System.Threading.Tasks;
using Domain.Aggregates.Films;

namespace Infrastructure.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly DatabaseContext _context;

        public FilmRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Film> GetFilmById(int filmId)
        {
            return await _context.Films.FindAsync(filmId);
        }
    }
}