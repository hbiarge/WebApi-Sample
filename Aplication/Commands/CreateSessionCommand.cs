using System;
using Aplication.Queries.ViewModels;
using FluentValidation;
using MediatR;

namespace Aplication.Commands
{
    public class CreateSessionCommand : IRequest<CreateSessionResponse>
    {
        public CreateSessionCommand(
            int cinemaId,
            int screenId,
            int filmId,
            DateTime start)
        {
            CinemaId = cinemaId;
            ScreenId = screenId;
            FilmId = filmId;
            Start = start;
        }

        public int CinemaId { get; }

        public int ScreenId { get; }

        public int FilmId { get; }

        public DateTime Start { get; }
    }

    public class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidator()
        {
            RuleFor(x => x.CinemaId)
                .GreaterThan(0);
            RuleFor(x => x.ScreenId)
                .GreaterThan(0);
            RuleFor(x => x.FilmId)
                .GreaterThan(0);
            RuleFor(x => x.Start)
                .GreaterThanOrEqualTo(DateTime.Today);
        }
    }

    public class CreateSessionResponse
    {
        public CreateSessionResponse(SessionViewModel session)
        {
            Session = session;
        }

        public SessionViewModel Session { get; }
    }
}