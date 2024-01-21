using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.ModelsDTO
{
    public class MusicClipDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(500, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 20 до 500 символов")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public DateTime? ReleaseDate { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Artist { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string? Genre { get; set; }

        public string? Path_Video { get; set; }

        public int? Id_user { get; set; }
        
    }
}
