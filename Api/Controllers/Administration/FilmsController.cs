using System.Net;
using System.Web.Http;
using Api.Infrastructure;
using Api.Infrastructure.Versioning;
using MediatR;

namespace Api.Controllers.Administration
{
    [Version1]
    [RoutePrefix("api/v{version:apiVersion}/films")]
    public class FilmsController : ApiController
    {
        private readonly IMediator _mediator;

        public FilmsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/v1/films

        [HttpGet]
        [Route]
        public IHttpActionResult GetFilms()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/v1/films/1

        [HttpGet]
        [Route("{filmId:int}")]
        public IHttpActionResult GetFilm(int filmId)
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/v1/films

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
