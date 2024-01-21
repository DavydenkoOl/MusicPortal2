﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.ModelsDTO
{
    public class GenreDTO
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? Genre_name { get; set; }
    }
}