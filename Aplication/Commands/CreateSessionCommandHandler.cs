using System;
using System.Linq;
using System.Threading.Tasks;
using Aplication.Queries.ViewModels;
using Domain;
using Domain.Aggregates.Cinemas;
using Domain.Aggregates.Films;
using Domain.Aggregates.Sessions;
using MediatR;

namespace Aplication.Commands
{
    public class CreateSessionCommandHandler : IAsyncRequestHandler<CreateSessionCommand, CreateSessionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly ISessionRepository _sessionRepository;

        public CreateSessionCommandHandler(
            IUnitOfWork unitOfWork,
            ICinemaRepository cinemaRepository,
            IFilmRepository filmRepository,
            ISessionRepository sessionRepository)
        {
            _unitOfWork = unitOfWork;
            _cinemaRepository = cinemaRepository;
            _filmRepository = filmRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<CreateSessionResponse> Handle(CreateSessionCommand message)
        {
            var cinema = await _cinemaRepository.GetCinemaById(message.CinemaId);

            if (cinema == null)
            {
                throw new InvalidOperationException($"The cinema with id [{message.CinemaId}] can not be found");
            }

            var film = await _filmRepository.GetFilmById(message.FilmId);

            if (film == null)
            {
                throw new InvalidOperationException($"The film with id [{message.FilmId}] can not be found");
            }

            var screen = cinema.Screens.SingleOrDefault(s => s.Id == message.ScreenId);

            if (screen == null)
            {
                throw new InvalidOperationException($"The screen with id [{message.ScreenId}] can not be found in the cinema [{message.CinemaId}]");
            }

            var session = Session.Create(
                screen,
                film,
                message.Start);

            await _sessionRepository.AddAsync(session);

            await _unitOfWork.CommitAsync();

            return new CreateSessionResponse(SessionViewModel.FromSession(session));
        }
    }
}