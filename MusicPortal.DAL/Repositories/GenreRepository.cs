using Microsoft.EntityFrameworkCore;
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
    public class GenreRepository: IRepository<Genre>
    {
        private readonly MusicPortalContext _context;


        public GenreRepository(MusicPortalContext context)
        {
            _context = context;
        }

        public async Task Create(Genre item)
        {
            await _context.Genres.AddAsync(item);
        }

        public async Task Delete(int? id)
        {
            Genre tmp = await _context.Genres.FindAsync(id);
            if (tmp != null)
            {
                _context.Genres.Remove(tmp);
            }
        }

        public async Task<List<Genre>> GetList()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetObject(int? id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Genre item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public bool Genrexists(int id)
        {
            return _context.Genres.Any(e => e.Id == id);
        }
    }
}
