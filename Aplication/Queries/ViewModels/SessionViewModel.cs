using Domain.Aggregates.Sessions;

namespace Aplication.Queries.ViewModels
{
    public class SessionViewModel
    {
        public int SessionId { get; set; }

        public int ScreenId { get; set; }

        public string ScreenName { get; set; }

        public int FilmId { get; set; }

        public string FilmTitle { get; set; }

        public int FilmDuration { get; set; }

        public static SessionViewModel FromSession(Session session)
        {
            return new SessionViewModel
            {
                SessionId = session.Id,
                ScreenId = session.ScreenId,
                ScreenName = session.Screen.Name,
                FilmId = session.FilmId,
                FilmTitle = session.Film.Title,
                FilmDuration = session.Film.DurationInMinutes
            };
        }
    }
}