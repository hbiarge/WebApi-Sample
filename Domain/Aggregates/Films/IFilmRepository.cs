using System.Threading.Tasks;

namespace Domain.Aggregates.Films
{
    public interface IFilmRepository
    {
        Task<Film> GetFilmById(int filmId);
    }
}
