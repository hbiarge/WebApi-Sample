using Aplication.Queries.ViewModels;
using MediatR;

namespace Aplication.Queries
{
    public class GetCinemasQuery
        : IRequest<QueryResponse<CinemaViewModel[]>>
    { }
}
