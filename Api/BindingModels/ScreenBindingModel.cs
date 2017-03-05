using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Api.BindingModels
{
    public class ScreenBindingModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Rows { get; set; }

        [Range(1, 50)]
        public int SeatsPerRow { get; set; }
    }
}
