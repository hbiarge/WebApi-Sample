using System;
using System.Collections.Generic;
using Domain.Aggregates.Cinemas;
using Domain.Aggregates.Films;

namespace Domain.Aggregates.Sessions
{
    public class Session
    {
        protected Session() { }

        public Session(Screen screen, Film film, DateTime start)
        {
            if (screen == null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            if (film == null)
            {
                throw new ArgumentNullException(nameof(film));
            }

            // TODO: Validar fecha de sesión

            Screen = screen;
            ScreenId = screen.Id;
            Film = film;
            FilmId = film.Id;
            Start = start;
            Seats = new List<SessionSeat>();

            CreateSeats();
        }

        public int Id { get; private set; }

        public int ScreenId { get; private set; }

        public Screen Screen { get; private set; }

        public int FilmId { get; private set; }

        public Film Film { get; private set; }

        public bool IsPublished { get; private set; }

        public DateTime Start { get; private set; }

        public DateTime End => Start.Add(TimeSpan.FromMinutes(Film.DurationInMinutes));

        public ICollection<SessionSeat> Seats { get; private set; }

        public void Publish()
        {
            if (IsPublished)
            {
                return;
            }

            IsPublished = true;
        }

        public void Unpublish()
        {
            if (IsPublished)
            {
                IsPublished = false;
            }
        }

        private void CreateSeats()
        {
            foreach (var seat in Screen.Seats)
            {
                var sessionSeat = new SessionSeat(this, seat);
                Seats.Add(sessionSeat);
            }
        }
    }
}