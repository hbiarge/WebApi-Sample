using Aplication.Queries.ViewModels;
using Domain.Aggregates.Cinemas;
using FluentValidation;
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

    public class CreateScreenCommandValidator : AbstractValidator<CreateScreenCommand>
    {
        public CreateScreenCommandValidator()
        {
            RuleFor(x => x.CinemaId)
                .GreaterThan(0);
            RuleFor(x => x.ScreenName)
                .NotNull()
                .Length(3, 100);
            RuleFor(x => x.ScreenRows)
                .InclusiveBetween(Screen.MinRowsNumber, Screen.MaxRowsNumber);
            RuleFor(x => x.ScreenSeatsPerRow)
                .InclusiveBetween(Screen.MinSeatsPerRowNumber, Screen.MaxSeatsPerRowNumber);
        }
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