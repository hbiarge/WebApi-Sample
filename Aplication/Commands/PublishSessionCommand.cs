using Aplication.Queries.ViewModels;
using FluentValidation;
using MediatR;

namespace Aplication.Commands
{
    public class PublishSessionCommand : IRequest
    {
        public PublishSessionCommand(
            int cinemaId,
            int sessionId,
            ActionType action)
        {
            CinemaId = cinemaId;
            SessionId = sessionId;
            Action = action;
        }

        public int CinemaId { get; }

        public int SessionId { get; }

        public ActionType Action { get; }

        public enum ActionType
        {
            Publish,
            Unpublish
        }
    }

    public class PublishSessionCommandValidator : AbstractValidator<PublishSessionCommand>
    {
        public PublishSessionCommandValidator()
        {
            RuleFor(x => x.CinemaId)
                .GreaterThan(0);
            RuleFor(x => x.SessionId)
                .GreaterThan(0);
        }
    }

    public class PublishSessionResponse
    {
        public PublishSessionResponse(ScreenViewModel screen)
        {
            Screen = screen;
        }

        public ScreenViewModel Screen { get; }
    }
}