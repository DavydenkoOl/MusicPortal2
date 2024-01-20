﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Models
{
    public class MusicClip
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? Artist { get; set; }

        public string? Genre { get; set; }

        public string? Path_Video { get; set; }

        public int? Id_user { get; set; }
    }
}
