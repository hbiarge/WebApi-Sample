using System.Threading.Tasks;
using System.Web.Http;
using Aplication.Queries;
using MediatR;

namespace Api.Controllers
{
    [RoutePrefix("cinemas")]
    public class CinemasController : ApiController
    {
        private readonly IMediator _mediator;

        public CinemasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetCinemas()
        {
            var response = await _mediator.Send(new GetCinemasQuery());

            return Ok(response.Cinemas);
        }
    }
}
