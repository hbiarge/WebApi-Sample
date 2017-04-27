using System.Threading.Tasks;
using System.Web.Http;
using Aplication.Queries;
using MediatR;

namespace Api.Controllers.Administration
{
    [RoutePrefix("api/cinemas")]
    public class CinemasController : ApiController
    {
        private readonly IMediator _mediator;

        public CinemasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: cinemas
        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetCinemas()
        {
            var response = await _mediator.Send(new GetCinemasQuery());

            return Ok(response.Data);
        }

        // GET: cinemas/1
        [HttpGet]
        [Route("{cinemaId:int}")]
        public async Task<IHttpActionResult> GetCinema(int cinemaId)
        {
            var response = await _mediator.Send(new GetCinemaQuery
            {
                CinemaId = cinemaId
            });

            return Ok(response.Data);
        }
    }
}
