using System;
using System.ComponentModel.DataAnnotations;

namespace Api.BindingModels
{
    public class CreateSessionBindingModel
    {
        [Range(1, int.MaxValue)]
        public int ScreenId { get; set; }

        [Range(1, int.MaxValue)]
        public int FilmId { get; set; }

        public DateTime Start { get; set; }
    }
}