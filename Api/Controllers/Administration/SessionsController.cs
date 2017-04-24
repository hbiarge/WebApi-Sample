using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Api.BindingModels;
using Api.Infrastructure;
using Aplication.Commands;
using MediatR;

namespace Api.Controllers.Administration
{
    [RoutePrefix("cinema/{cinemaId:int}/sessions")]
    public class SessionsController : ApiController
    {
        private readonly IMediator _mediator;

        public SessionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: cinemas/1/sessions
        [HttpGet]
        [Route]
        public IHttpActionResult GetSessions()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: cinemas/1/sessions/1
        [HttpGet]
        [Route("{sessionId:int}", Name = "GetSession")]
        public IHttpActionResult GetSession(int cinemaId, int sessionId)
        {
            return Ok($"Session {sessionId}");
        }

        // POST: cinemas/1/sessions
        [HttpPost]
        [Route]
        [ValidateModel]
        public async Task<IHttpActionResult> CreateSession(
            int cinemaId,
            object model)
        {
            //var response = await _mediator.Send(new CreateScreenCommand(
            //    cinemaId: cinemaId,
            //    screenName: model.Name,
            //    screenRows: model.Rows,
            //    screenSeatsPerRow: model.SeatsPerRow));

            //var url = Url.Route("GetScreen", new { CinemaId = cinemaId, ScreenId = response.Screen.Id });
            //return Created(url, response.Screen);
            throw new NotImplementedException();
        }

        // DELETE: cinemas/1/sessions/1
        [HttpDelete]
        [Route("{sessionId:int}")]
        public async Task<IHttpActionResult> DeleteSession(
            int cinemaId,
            int sessionId)
        {
            throw new NotImplementedException();
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
