using MusicPortal.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal2.Models
{
    public class GenreView
    {
        public int Id { get; set; }

        [Required]
        public string Genre_name { get; set; }

        public IEnumerable<Genre> GenreList { get; set; }
    }
}
