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
            SeatId = Seat.Id;
        }

        public int SessionId { get; private set; }

        public Session Session { get; private set; }

        public int SeatId { get; private set; }

        public Seat Seat { get; private set; }

        public bool Sold { get; private set; }

        public Ticket Ticket { get; private set; }

        public Ticket Sell(decimal price)
        {
            if (Sold)
            {
                throw new InvalidOperationException("The seat is already reserved for this session");
            }

            Sold = true;
            return new Ticket(this, price);
        }

        public void CancelReservation()
        {
            Sold = false;
        }
    }
}