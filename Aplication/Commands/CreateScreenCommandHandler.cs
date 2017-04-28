using System;
using System.Threading.Tasks;
using Aplication.Queries.ViewModels;
using Domain;
using Domain.Aggregates.Cinemas;
using MediatR;

namespace Aplication.Commands
{
    public class CreateScreenCommandHandler : IAsyncRequestHandler<CreateScreenCommand, CreateScreenResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICinemaRepository _cinemaRepository;

        public CreateScreenCommandHandler(IUnitOfWork unitOfWork,ICinemaRepository cinemaRepository)
        {
            _unitOfWork = unitOfWork;
            _cinemaRepository = cinemaRepository;
        }

        public async Task<CreateScreenResponse> Handle(CreateScreenCommand message)
        {
            var cinema = await _cinemaRepository.GetCinemaById(message.CinemaId);

            if (cinema==null)
            {
                throw new InvalidOperationException($"The cinema with id [{message.CinemaId}] can not be found");
            }

            var screen = cinema.CreateScreen(
                message.ScreenName,
                message.ScreenRows,
                message.ScreenSeatsPerRow);

            await _unitOfWork.CommitAsync();

            return new CreateScreenResponse(ScreenViewModel.FromScreen(screen));
        }
    }
}