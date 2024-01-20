using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Context
{
    public class MusicPortalContext:DbContext
    {
        public DbSet<Users> Users { get; set; }

        public DbSet<MusicClip> Clips { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
