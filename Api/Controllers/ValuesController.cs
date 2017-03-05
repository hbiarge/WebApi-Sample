using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Aplication;
using MediatR;
using System.Linq;
using Aplication.Commands;

namespace Api.Controllers
{
    [RoutePrefix("values")]
    public class ValuesController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly MultiInstanceFactory _mufa;

        public ValuesController(IMediator mediator, MultiInstanceFactory mufa)
        {
            _mediator = mediator;
            _mufa = mufa;
        }

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> GetValues()
        {
            var response = await _mediator.Send(new ValuesRequest());
            return Ok(response.Values);
        }
    }
}
