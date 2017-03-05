using Domain;
using Domain.Aggregates.Sessions;

namespace Infrastructure.Repositories
{
    public class SessionsRepository : ISessionRepository
    {
        private readonly DatabaseContext _context;

        public SessionsRepository(DatabaseContext context)
        {
            _context = context;
            UnitOfWork = _context;
        }

        public IUnitOfWork UnitOfWork { get; }
    }
}