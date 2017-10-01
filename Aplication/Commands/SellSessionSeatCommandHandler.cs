using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Aggregates.Sessions;
using MediatR;

namespace Aplication.Commands
{
    public class SellSessionSeatCommandHandler : IAsyncRequestHandler<SellSessionSeatCommand, SellSessionSeatResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionRepository _sessionRepository;

        public SellSessionSeatCommandHandler(
            IUnitOfWork unitOfWork,
            ISessionRepository sessionRepository)
        {
            _unitOfWork = unitOfWork;
            _sessionRepository = sessionRepository;
        }

        public async Task<SellSessionSeatResponse> Handle(SellSessionSeatCommand message)
        {
            var session = await _sessionRepository.GetSessionById(message.CinemaId, message.SessionId);

            if (session == null)
            {
                throw new InvalidOperationException($"The session with id [{message.SessionId}] can not be found");
            }

            var seat = session.Seats.SingleOrDefault(x =>
                x.Seat.Row == message.Row && x.Seat.Number == message.Number);

            if (seat == null)
            {
                throw new InvalidOperationException($"The seat in row [{message.Row}] and number [{message.Number}] can not be found in the session [{message.SessionId}]");
            }

            // Calculate price based on client type calling a priceService collaborator
            const decimal price = 6.5M;

            var ticket = seat.Sell(price);

            await _unitOfWork.CommitAsync();

            return new SellSessionSeatResponse(ticket);
        }
    }
}