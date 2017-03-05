using Aplication.Queries.ViewModels;
using Domain.Aggregates.Cinemas;
using MediatR;

namespace Aplication.Commands
{
    public class CreateScreenCommand : IRequest<CreateScreenResponse>
    {
        public CreateScreenCommand(
            int cinemaId,
            string screenName,
            int screenRows,
            int screenSeatsPerRow)
        {
            CinemaId = cinemaId;
            ScreenName = screenName;
            ScreenRows = screenRows;
            ScreenSeatsPerRow = screenSeatsPerRow;
        }

        public int CinemaId { get; }

        public string ScreenName { get; }

        public int ScreenRows { get; }

        public int ScreenSeatsPerRow { get; }
    }

    public class CreateScreenResponse
    {
        public CreateScreenResponse(ScreenViewModel screen)
        {
            Screen = screen;
        }

        public ScreenViewModel Screen { get; }
    }
}