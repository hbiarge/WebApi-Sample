using System.Threading.Tasks;
using System.Web.Http;
using Api.Infrastructure.Versioning;
using Aplication.Commands;
using MediatR;

namespace Api.Controllers.Ticketing
{
    [Version1]
    [RoutePrefix("api/v{version:apiVersion}/cinemas/{cinemaId:int}/ticketing")]
    public class TicketsController : ApiController
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST: cinemas/1/ticketing/sessions/1/seat/1/2

        [HttpPost]
        [Route("sessions/{sessionId:int}/seat/{row:int}/{number:int}")]
        public async Task<IHttpActionResult> SellSessionSeat(
            int cinemaId,
            int sessionId,
            int row,
            int number)
        {
            // TODO: Add information about the client (student, etc)
            // to be able to call a prices service in the commandHandler

            var response = await _mediator.Send(new SellSessionSeatCommand(
                cinemaId: cinemaId,
                sessionId: sessionId,
                row: row,
                number: number));

            return Ok(response);
        }

        // Undo sell ticket for session
        // Available seats per session
    }
}
