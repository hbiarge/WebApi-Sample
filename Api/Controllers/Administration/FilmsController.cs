using System.Net;
using System.Web.Http;
using Api.Infrastructure;
using MediatR;

namespace Api.Controllers.Administration
{
    [RoutePrefix("films")]
    public class FilmsController : ApiController
    {
        private readonly IMediator _mediator;

        public FilmsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: films
        [HttpGet]
        [Route]
        public IHttpActionResult GetFilms()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: films/1
        [HttpGet]
        [Route("{filmId:int}")]
        public IHttpActionResult GetFilm(int filmId)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: films
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
