using System.Threading.Tasks;

namespace Domain.Aggregates.Cinemas
{
    public interface ICinemaRepository
    {
        Task<Cinema> GetCinemaById(int cinemaId);
    }
}
