using System;
using System.Collections.Generic;
using Domain.Aggregates.Fims;
using Domain.Aggregates.Sessions;

namespace Domain.Aggregates.Cinemas
{
    public class Cinema
    {
        protected Cinema() { }

        public Cinema(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Screens = new List<Screen>();
            Films = new List<Film>();
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public ICollection<Screen> Screens { get; private set; }

        public ICollection<Film> Films { get; private set; }

        public Screen CreateScreen(string name, int rows, int seatsPerRow)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var screen = new Screen(this, name, rows, seatsPerRow);

            Screens.Add(screen);

            return screen;
        }

        public void AddFilm(Film film)
        {
            if (film == null)
            {
                throw new ArgumentNullException(nameof(film));
            }

            Films.Add(film);
        }

        public Session CreateSession(Screen screen, Film film, DateTime start)
        {
            var session = new Session(screen, film, start);
            
            return session;
        }
    }
}
