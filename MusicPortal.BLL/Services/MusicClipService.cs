using AutoMapper;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Services
{
    public class MusicClipService: IMusicClipCervices
    {
        IUnitOfWork Database { get; set; }

        public MusicClipService(IUnitOfWork _base)
        {
            Database = _base;
        }
        public async Task Create(MusicClipDTO clip)
        {
            var _clip = new MusicClip()
            {
                Id = clip.Id,
               Title = clip.Title,
               Description = clip.Description,
               ReleaseDate = clip.ReleaseDate,
               Artist = clip.Artist,
               Genre = clip.Genre,
               Path_Video = clip.Path_Video,
               Id_user = clip.Id_user
            };
            await Database.MusicClip.Create(_clip);
            await Database.Save();
        }
        public async Task Update(MusicClipDTO clip)
        {
            var _clip = new MusicClip()
            {
                Id = clip.Id,
                Title = clip.Title,
                Description = clip.Description,
                ReleaseDate = clip.ReleaseDate,
                Artist = clip.Artist,
                Genre = clip.Genre,
                Path_Video = clip.Path_Video,
                Id_user = clip.Id_user
            };
            Database.MusicClip.Update(_clip);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            await Database.MusicClip.Delete(id);
            await Database.Save();
        }
        public async Task<MusicClipDTO> GetClip(int id)
        {
            var clip = await Database.MusicClip.GetObject(id);
            if (clip == null)
                throw new ValidationException("Нет такого жанра");
            return new MusicClipDTO
            {
                Id = clip.Id,
                Title = clip.Title,
                Description = clip.Description,
                ReleaseDate = clip.ReleaseDate,
                Artist = clip.Artist,
                Genre = clip.Genre,
                Path_Video = clip.Path_Video,
                Id_user = clip.Id_user
            };
        }
        public async Task<IEnumerable<MusicClipDTO>> GetClip()
        {
            var config = new MapperConfiguration(tmp => tmp.CreateMap<MusicClip, MusicClipDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<MusicClip>, IEnumerable<MusicClipDTO>>(await Database.MusicClip.GetList());
        }
    }
}
