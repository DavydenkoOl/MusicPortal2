using MusicPortal.DAL.Context;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Repositories
{
    public class EFUnitOfWork: IUnitOfWork
    {
        private MusicPortalContext _context;
        private GenreRepository _genreRepository;
        private UserRepository _userRepository;
        private MusicClipRepository _musicClipRepository;

        public EFUnitOfWork(MusicPortalContext context)
        {
            _context = context;

        }

        public IRepository<Genre> Genre
        {
            get
            {
                if (_genreRepository == null)
                    _genreRepository = new GenreRepository(_context);
                return _genreRepository;
            }
        }

        public IRepository<MusicClip> MusicClip
        {
            get
            {
                if (_musicClipRepository == null)
                    _musicClipRepository = new MusicClipRepository(_context);
                return _musicClipRepository;
            }
        }


        public IRepository<Users> Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
