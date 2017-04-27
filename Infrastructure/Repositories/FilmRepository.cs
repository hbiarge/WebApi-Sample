using Domain;
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
    }
}