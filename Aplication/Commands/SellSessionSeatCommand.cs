using System;
using Aplication.Queries.ViewModels;
using FluentValidation;
using MediatR;

namespace Aplication.Commands
{
    public class SellSessionSeatCommand : IRequest<SellSessionSeatResponse>
    {
        public SellSessionSeatCommand(
            int cinemaId,
            int sessionId,
            int row,
            int number)
        {
            CinemaId = cinemaId;
            SessionId = sessionId;
            Row = row;
            Number = number;
        }

        public int CinemaId { get; }

        public int SessionId { get; }

        public int Row { get; }

        public int Number { get; }
    }

    public class SellSessionSeatCommandValidator : AbstractValidator<SellSessionSeatCommand>
    {
        public SellSessionSeatCommandValidator()
        {
            RuleFor(x => x.CinemaId)
                .GreaterThan(0);
            RuleFor(x => x.SessionId)
                .GreaterThan(0);
            RuleFor(x => x.Row)
                .GreaterThan(0);
            RuleFor(x => x.Number)
                .GreaterThan(0);
        }
    }

    public class SellSessionSeatResponse
    {
        public SellSessionSeatResponse(Guid ticketId)
        {
            TicketId = ticketId;
        }

        public Guid TicketId { get; }
    }
}