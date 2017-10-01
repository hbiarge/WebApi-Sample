using System;

namespace Domain.Aggregates.Cinemas
{
    public class Seat
    {
        protected Seat() { }

        public Seat(Screen screen, int row, int number)
        {
            if (screen == null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            if (row <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            Screen = screen;
            Row = row;
            Number = number;
        }

        public int Row { get; private set; }

        public int Number { get; private set; }

        public int ScreenId { get; private set; }

        public Screen Screen { get; private set; }
    }
}