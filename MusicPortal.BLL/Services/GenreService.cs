using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class GenreService: IGenreService
    {
        IUnitOfWork Database { get; set; }

        public GenreService(IUnitOfWork _base)
        {
            Database = _base;
        }
        public async Task Create(GenreDTO genreDto)
        {
            var genre = new Genre()
            {
                Id = genreDto.Id,
                Genre_name = genreDto.Genre_name
            };
            await Database.Genre.Create(genre);
            await Database.Save();
        }
        public async Task Update(GenreDTO genreDto)
        {
            var gen = await Database.Genre.GetObject(genreDto.Id);

            gen.Id = genreDto.Id;
                gen.Genre_name = genreDto.Genre_name;
            
            Database.Genre.Update(gen);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            await Database.Genre.Delete(id);
            await Database.Save();
        }
        public async Task<GenreDTO> GetGenre(int id)
        {
            var genre = await Database.Genre.GetObject(id);
            if (genre == null)
                throw new ValidationException("Нет такого жанра");
            return new GenreDTO
            {
                Id = genre.Id,
                Genre_name = genre.Genre_name
            };
        }
        public async Task<IEnumerable<GenreDTO>> GetGenre()
        {
            var config = new MapperConfiguration(tmp => tmp.CreateMap<Genre,GenreDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(await Database.Genre.GetList());
        }
    }
}
