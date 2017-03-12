using System.Threading.Tasks;
using System.Web.Http;
using Aplication.Queries;
using MediatR;
using System.Net;
using Api.Infrastructure;

namespace Api.Controllers
{
    [RoutePrefix("films")]
    public class FilmsController : ApiController
    {
        private readonly IMediator _mediator;

        public FilmsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route]
        public IHttpActionResult GetFilms()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("filmId:int")]
        public IHttpActionResult GetFilm(int filmId)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route]
        [ValidateModel]
        public IHttpActionResult CreateFilm(
            string model)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
