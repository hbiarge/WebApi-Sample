using System.Threading.Tasks;

namespace Domain.Aggregates.Cinemas
{
    public interface ICinemaRepository: IRepository
    {
        Task<Cinema> GetCinemaById(int cinemaId);
    }
}
