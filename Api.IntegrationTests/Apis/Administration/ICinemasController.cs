using System.Threading.Tasks;
using Aplication.Queries.ViewModels;
using Refit;

namespace Api.IntegrationTests.Apis.Administration
{
    public interface ICinemasController
    {
        [Get("/api/v1/cinemas")]
        Task<CinemaViewModel[]> GetCinemas([Header("Authorization")]string authorization);

        [Get("/api/v1/cinemas/{id}")]
        Task<CinemaViewModel> GetCinema([AliasAs("id")]int cinemaId);
    }
}
