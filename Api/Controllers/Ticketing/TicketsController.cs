using System.Web.Http;
using MediatR;

namespace Api.Controllers.Ticketing
{
    [RoutePrefix("api/cinemas/{cinemaId:int}/ticketing")]
    public class TicketsController : ApiController
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Sell ticket for session
        // Undo sell ticket for session
        // Available seats per session
    }
}
