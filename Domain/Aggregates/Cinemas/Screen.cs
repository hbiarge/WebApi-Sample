using System;
using System.Collections.Generic;

namespace Domain.Aggregates.Cinemas
{
    public class Screen
    {
        protected Screen() { }

        public Screen(Cinema cinema, string name)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Cinema = cinema;
            Name = name;
            Seats = new List<Seat>();
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public int CinemaId { get; private set; }

        public Cinema Cinema { get; private set; }

        public ICollection<Seat> Seats { get; private set; }

        public void CreateSeats(int rows, int seatsPerRow)
        {
            for (int row = 1; row <= rows; row++)
            {
                for (int seat = 1; seat <= seatsPerRow; seat++)
                {
                    Seats.Add(new Seat(this, row, seat));
                }
            }
        }
    }
}