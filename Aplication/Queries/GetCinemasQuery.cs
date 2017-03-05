using System.Collections.Generic;
using Aplication.Queries.ViewModels;
using MediatR;

namespace Aplication.Queries
{
    public class GetCinemasQuery : IRequest<GetCinemasResponse> { }

    public class GetCinemasResponse
    {
        public GetCinemasResponse(CinemaViewModel[] cinemas)
        {
            Cinemas = cinemas;
        }

        public IReadOnlyCollection<CinemaViewModel> Cinemas { get; }
    }
}
