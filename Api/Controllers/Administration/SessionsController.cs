using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Api.BindingModels;
using Api.Infrastructure;
using Api.Infrastructure.Versioning;
using Aplication.Commands;
using MediatR;

namespace Api.Controllers.Administration
{
    [Version1]
    [RoutePrefix("api/v{version:apiVersion}/cinemas/{cinemaId:int}/sessions")]
    public class SessionsController : ApiController
    {
        private readonly IMediator _mediator;

        public SessionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/v1/cinemas/1/sessions

        [HttpGet]
        [Route]
        public IHttpActionResult GetSessions()
        {
            // TODO: Implement
            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: api/v1/cinemas/1/sessions/1

        [HttpGet]
        [Route("{sessionId:int}", Name = "GetSession")]
        public IHttpActionResult GetSession(int cinemaId, int sessionId)
        {
            // TODO: Implement
            return Ok($"Session {sessionId}");
        }

        // POST: api/v1/cinemas/1/sessions

        [HttpPost]
        [Route]
        [ValidateModel]
        public async Task<IHttpActionResult> CreateSession(
            int cinemaId,
            CreateSessionBindingModel model)
        {
            var response = await _mediator.Send(new CreateSessionCommand(
                cinemaId: cinemaId,
                screenId: model.ScreenId,
                filmId: model.FilmId,
                start: model.Start));

            var url = Url.Route("GetSession", new { CinemaId = cinemaId, SessionId = response.Session.SessionId });
            return Created(url, response.Session);
        }

        // DELETE: cinemas/1/sessions/1
        [HttpDelete]
        [Route("{sessionId:int}")]
        public async Task<IHttpActionResult> DeleteSession(
            int cinemaId,
            int sessionId)
        {
            // TODO: Implement
            throw new NotImplementedException();
        }

        // PUT: api/v1/cinemas/1/sessions/1/publish

        [HttpPut]
        [Route("{sessionId:int}/publish")]
        [ValidateModel]
        public async Task<IHttpActionResult> PublishSession(int cinemaId, int sessionId)
        {
            await _mediator.Send(new PublishSessionCommand(
                cinemaId: cinemaId,
                sessionId: sessionId,
                action: PublishSessionCommand.ActionType.Publish));

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: cinemas/1/sessions/1/publish

        [HttpDelete]
        [Route("{sessionId:int}/publish")]
        [ValidateModel]
        public async Task<IHttpActionResult> UnpublishSession(int cinemaId, int sessionId)
        {
            await _mediator.Send(new PublishSessionCommand(
                cinemaId: cinemaId,
                sessionId: sessionId,
                action: PublishSessionCommand.ActionType.Publish));

            return StatusCode(HttpStatusCode.NoContent);
        }

        // List sessions (Pubished and unpublished)
        // Create sessions
        // Update session
        // Delete session

        // Publish sessions for day
        // Unpublish sessions for day
        // Publish session
        // Unpublish session
    }
}
