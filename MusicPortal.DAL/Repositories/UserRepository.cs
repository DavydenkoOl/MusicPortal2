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
    internal class UserRepository: IRepository<Users>
    {
        private readonly MusicPortalContext _context;
        public UserRepository(MusicPortalContext context)
        {
            _context = context;
        }
        public async Task Create(Users item)
        {
            await _context.Users.AddAsync(item);
        }

        public async Task Delete(int? id)
        {
            Users deluser = await _context.Users.FindAsync(id);
            if (deluser != null) { }
        }

        public async Task<Users> GetObject(int? id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<Users>> GetList()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Users item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
    }
}
