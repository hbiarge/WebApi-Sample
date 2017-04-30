using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Api.Infrastructure.Versioning;
using Aplication.Queries;
using Aplication.Queries.ViewModels;
using MediatR;

namespace Api.Controllers.Administration
{
    [Version1]
    [RoutePrefix("api/v{version:apiVersion}/cinemas")]
    public class CinemasController : ApiController
    {
        private readonly IMediator _mediator;

        public CinemasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/v1/cinemas

        [HttpGet]
        [Route]
        [ResponseType(typeof(CinemaViewModel[]))]
        public async Task<IHttpActionResult> GetCinemas()
        {
            var response = await _mediator.Send(new GetCinemasQuery());

            return Ok(response.Data);
        }

        // GET: api/v1/cinemas/1
        [HttpGet]
        [Route("{cinemaId:int}")]
        [ResponseType(typeof(CinemaViewModel))]
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
