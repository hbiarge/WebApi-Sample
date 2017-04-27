using System;
using System.Collections.Generic;
using Domain.Aggregates.Cinemas;

namespace Domain.Aggregates.Films
{
    public class Film
    {
        public const int MinimumFilmDuration = 1;
        public const int MaximumFilmDuration = 200;

        protected Film() { }

        public Film(string title, int durationInMinutes)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title));
            }

            if (durationInMinutes < MinimumFilmDuration
                || durationInMinutes > MaximumFilmDuration)
            {
                throw new ArgumentOutOfRangeException(nameof(durationInMinutes));
            }

            Title = title;
            DurationInMinutes = durationInMinutes;
            Cinemas = new List<Cinema>();
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public int DurationInMinutes { get; private set; }

        public ICollection<Cinema> Cinemas { get; private set; }
    }
}