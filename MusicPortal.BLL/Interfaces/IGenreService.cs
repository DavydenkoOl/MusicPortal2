using MusicPortal.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface IGenreService
    {
        Task Create(GenreDTO genreDto);
        Task Update(GenreDTO genreDto);
        Task Delete(int id);
        Task<GenreDTO> GetGenre(int id);
        Task<IEnumerable<GenreDTO>> GetGenre();
    }
}
