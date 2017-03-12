using System;
using System.Collections.Generic;

namespace Domain.Aggregates.Cinemas
{
    public class Screen
    {
        public const int MinRowsNumber = 1;
        public const int MaxRowsNumber = 100;
        public const int MinSeatsPerRowNumber = 1;
        public const int MaxSeatsPerRowNumber = 50;

        protected Screen() { }

        public Screen(Cinema cinema, string name, int rows, int seatsPerRow)
        {
            if (cinema == null)
            {
                throw new ArgumentNullException(nameof(cinema));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (rows < MinRowsNumber || rows > MaxRowsNumber)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            if (seatsPerRow < MinSeatsPerRowNumber || seatsPerRow > MaxSeatsPerRowNumber)
            {
                throw new ArgumentOutOfRangeException(nameof(seatsPerRow));
            }

            Cinema = cinema;
            Name = name;
            Seats = new List<Seat>();

            CreateSeats(rows, seatsPerRow);
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public int CinemaId { get; private set; }

        public Cinema Cinema { get; private set; }

        public ICollection<Seat> Seats { get; private set; }

        private void CreateSeats(int rows, int seatsPerRow)
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