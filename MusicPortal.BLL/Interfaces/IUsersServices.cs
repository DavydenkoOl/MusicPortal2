using MusicPortal.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface IUsersServices
    {
        Task Create(UsersDTO userDto);
        Task Update(UsersDTO userDto);
        Task Delete(int id);
        Task<UsersDTO> GetUser(int id);
        Task<IEnumerable<UsersDTO>> GetUser();
    }
}
