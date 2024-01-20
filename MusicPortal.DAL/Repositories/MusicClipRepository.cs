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
    internal class MusicClipRepository: IRepository<MusicClip>
    {
        private readonly MusicPortalContext _context;
        public MusicClipRepository(MusicPortalContext context)
        {
            _context = context;
        }
        public async Task Create(MusicClip item)
        {
            await _context.Clips.AddAsync(item);
        }

        public async Task Delete(int? id)
        {
            MusicClip clip = await _context.Clips.FindAsync(id);
            if (clip != null)
            {
                _context.Clips.Remove(clip);
            }
        }

        public async Task<List<MusicClip>> GetList()
        {
            return await _context.Clips.ToListAsync();
        }

        public async Task<MusicClip> GetObject(int? id)
        {
            return await _context.Clips.FindAsync(id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(MusicClip item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
