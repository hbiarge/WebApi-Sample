using Domain;
using Domain.Aggregates.Fims;

namespace Infrastructure.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly DatabaseContext _context;

        public FilmRepository(DatabaseContext context)
        {
            _context = context;
            UnitOfWork = _context;
        }

        public IUnitOfWork UnitOfWork { get; }
    }
}