using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;

namespace Api.Controllers.Ticketing
{
    public class TicketsController : ApiController
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Sell ticket for session
        // Undo sell ticket for session
        // Available seats per session
    }
}
