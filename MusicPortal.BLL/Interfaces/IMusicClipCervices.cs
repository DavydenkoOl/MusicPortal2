using MusicPortal.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface IMusicClipCervices
    {
        Task Create(MusicClipDTO clipDto);
        Task Update(MusicClipDTO clipDto);
        Task Delete(int id);
        Task<MusicClipDTO> GetClip(int id);
        Task<IEnumerable<MusicClipDTO>> GetClip();
    }
}
