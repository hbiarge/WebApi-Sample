using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Api.BindingModels;
using Api.Infrastructure;
using Aplication.Commands;
using MediatR;

namespace Api.Controllers.Administration
{
    [RoutePrefix("api/cinemas/{cinemaId:int}/screens")]
    public class ScreensController : ApiController
    {
        private readonly IMediator _mediator;

        public ScreensController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: cinemas/1/screens
        [HttpGet]
        [Route]
        public IHttpActionResult GetScreens()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: cinemas/1/screens/1
        [HttpGet]
        [Route("{screenId:int}", Name = "GetScreen")]
        public IHttpActionResult GetScreen(int cinemaId, int screenId)
        {
            return Ok($"Screen {screenId}");
        }

        // POST: cinemas/1/screens
        [HttpPost]
        [Route]
        [ValidateModel]
        public async Task<IHttpActionResult> CreateScreen(
            int cinemaId,
            CreateScreenBindingModel model)
        {
            var response = await _mediator.Send(new CreateScreenCommand(
                cinemaId: cinemaId,
                screenName: model.Name,
                screenRows: model.Rows,
                screenSeatsPerRow: model.SeatsPerRow));

            var url = Url.Route("GetScreen", new
            {
                CinemaId = cinemaId,
                ScreenId = response.Screen.Id
            });

            return Created(url, response.Screen);
        }
    }
}
