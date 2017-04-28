using System;
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

            // Calculate price based on client type calling a priceService collaborator
            var price = 6.5M;

            var ticket = session.SellSeat(message.Row, message.Row, price);

            await _unitOfWork.CommitAsync();

            return new SellSessionSeatResponse(ticket.Id);
        }
    }
}