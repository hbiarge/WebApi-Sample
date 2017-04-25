using System;
using System.Net;
using System.Web.Http;
using Api.Infrastructure;
using MediatR;

namespace Api.Controllers.Scheduling
{
    [RoutePrefix("cinemas/{cinemaId:int}/schedule")]
    public class ScheduleController : ApiController
    {
        private readonly IMediator _mediator;

        public ScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: cinemas/1/schedule/2017/4/3
        [HttpGet]
        [Route("{year:int}/{month:range(1,12)}/{day:range(1,31)}")]
        //[ValidateDate]
        public IHttpActionResult GetSchedule(
            int cinemaId,
            int year,
            int month,
            int day)
        {
            if (DateBuilder.TryBuildFrom(year, month, day, out DateTime date) == false)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
