using System;
using Domain.Aggregates.Cinemas;

namespace Domain.Aggregates.Sessions
{
    public class SessionSeat
    {
        protected SessionSeat() { }

        public SessionSeat(Session session, Seat seat)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (seat == null)
            {
                throw new ArgumentNullException(nameof(seat));
            }

            Session = session;
            SessionId = Session.Id;
            Seat = seat;
            SeatScreenId = seat.ScreenId;
            SeatRow = seat.Row;
            SeatNumber = seat.Number;
        }

        public int SessionId { get; private set; }

        public Session Session { get; private set; }

        public int SeatScreenId { get; private set; }

        public int SeatRow { get; private set; }

        public int SeatNumber { get; private set; }

        public Seat Seat { get; private set; }

        public decimal? Price { get; private set; }

        public Guid? Ticket { get; private set; }

        public bool Sold => Ticket.HasValue;

        public Guid Sell(decimal price)
        {
            if (Sold)
            {
                throw new InvalidOperationException();
            }

            Price = price;
            Ticket = Guid.NewGuid();

            return Ticket.Value;
        }
    }
}