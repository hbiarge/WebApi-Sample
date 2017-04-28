using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Aggregates.Films;
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
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public ICollection<Screen> Screens { get; private set; }

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

        public Session CreateSession(int screenId, Film film, DateTime start)
        {
            var screen = Screens.FirstOrDefault(s => s.Id == screenId);

            if (screen==null)
            {
                throw new InvalidOperationException($"The screen with id [{screenId}] has not been found in this cinema");
            }

            var session = new Session(screen, film, start);

            return session;
        }
    }
}
