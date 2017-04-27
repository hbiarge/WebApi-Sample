using Aplication.Queries.ViewModels;
using MediatR;

namespace Aplication.Queries
{
    public class GetCinemaQuery
        : IRequest<QueryResponse<CinemaViewModel>>
    {
        public int CinemaId { get; set; }
    }
}