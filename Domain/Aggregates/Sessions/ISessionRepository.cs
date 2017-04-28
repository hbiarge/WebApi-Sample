using System.Threading.Tasks;

namespace Domain.Aggregates.Sessions
{
    public interface ISessionRepository
    {
        Task<Session> GetSessionById(int cinemaId, int sessionId);

        Task AddAsync(Session session);
    }
}
