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
    public class UsersService:IUsersServices
    {
        IUnitOfWork Database { get; set; }

        public UsersService(IUnitOfWork _base)
        {
            Database = _base;
        }
        public async Task Create(UsersDTO userDto)
        {
            var us = new Users()
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Login = userDto.Login,
                Password = userDto.Password,
                Salt = userDto.Salt,
                IsСonfirm = userDto.IsСonfirm
            };
            await Database.Users.Create(us);
            await Database.Save();
        }
        public async Task Update(UsersDTO userDto)
        {
            var us = new Users()
            {
                Id = userDto.Id,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Login = userDto.Login,
                Password = userDto.Password,
                Salt = userDto.Salt,
                IsСonfirm = userDto.IsСonfirm
            };
            Database.Users.Update(us);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            await Database.Users.Delete(id);
            await Database.Save();
        }
        public async Task<UsersDTO> GetUser(int id)
        {
            var us = await Database.Users.GetObject(id);
            if (us == null)
                throw new ValidationException("Нет такого жанра");
            return new UsersDTO
            {
                Id = us.Id,
                FirstName = us.FirstName,
                LastName = us.LastName,
                Login = us.Login,
                Password = us.Password,
                Salt = us.Salt,
                IsСonfirm =us.IsСonfirm
            };
        }
        public async Task<IEnumerable<UsersDTO>> GetUser()
        {
            var config = new MapperConfiguration(tmp => tmp.CreateMap<Users, UsersDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Users>, IEnumerable<UsersDTO>>(await Database.Users.GetList());
        }
    }
}
