using Domain.Aggregates.Cinemas;

namespace Aplication.Queries.ViewModels
{
    public class ScreenViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public static ScreenViewModel FromScreen(Screen screen)
        {
            return new ScreenViewModel
            {
                Id = screen.Id,
                Name = screen.Name,
            };
        }
    }
}