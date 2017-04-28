using System.Threading.Tasks;
using Domain.Aggregates.Sessions;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class SessionsRepository : ISessionRepository
    {
        private readonly DatabaseContext _context;

        public SessionsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Task<Session> GetSessionById(int cinemaId, int sessionId)
        {
            return _context.Sessions
                .Include(s => s.Screen)
                .Include(s => s.Film)
                .Include(s => s.Seats.Select(x => x.Seat))
                .SingleOrDefaultAsync(s => s.Id == sessionId && s.Screen.CinemaId == cinemaId);
        }

        public Task AddAsync(Session session)
        {
            _context.Sessions.Add(session);

            return Task.FromResult<object>(null);
        }
    }
}