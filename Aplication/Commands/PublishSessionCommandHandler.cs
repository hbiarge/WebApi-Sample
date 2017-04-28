using System;
using System.Threading.Tasks;
using Domain;
using Domain.Aggregates.Sessions;
using MediatR;

namespace Aplication.Commands
{
    public class PublishSessionCommandHandler : IAsyncRequestHandler<PublishSessionCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionRepository _sessionRepository;

        public PublishSessionCommandHandler(
            IUnitOfWork unitOfWork,
            ISessionRepository sessionRepository)
        {
            _unitOfWork = unitOfWork;
            _sessionRepository = sessionRepository;
        }

        public async Task Handle(PublishSessionCommand message)
        {
            var session = await _sessionRepository.GetSessionById(message.CinemaId, message.SessionId);

            if (session == null)
            {
                throw new InvalidOperationException($"The session with id [{message.SessionId}] can not be found");
            }

            if (message.Action == PublishSessionCommand.ActionType.Publish)
            {
                session.Publish();
            }
            if (message.Action == PublishSessionCommand.ActionType.Unpublish)
            {
                session.Unpublish();
            }

            await _unitOfWork.CommitAsync();
        }
    }
}