using System.Threading.Tasks;
using Aplication.Queries.ViewModels;
using Domain.Aggregates.Cinemas;
using MediatR;

namespace Aplication.Commands
{
    public class CreateScreenCommandHandler : IAsyncRequestHandler<CreateScreenCommand, CreateScreenResponse>
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CreateScreenCommandHandler(ICinemaRepository cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        public async Task<CreateScreenResponse> Handle(CreateScreenCommand message)
        {
            var cinema = await _cinemaRepository.GetCinemaById(message.CinemaId);

            var screen = cinema.CreateScreen(
                message.ScreenName,
                message.ScreenRows,
                message.ScreenSeatsPerRow);

            await _cinemaRepository.UnitOfWork.CommitAsync();

            return new CreateScreenResponse(ScreenViewModel.FromScreen(screen));
        }
    }
}