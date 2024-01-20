using MusicPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Genre> Genre { get; }
        IRepository<MusicClip> MusicClip { get; }
        IRepository<Users> Users { get; }
        Task Save();
    }
}
