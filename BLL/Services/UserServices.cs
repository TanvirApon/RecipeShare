using AutoMapper;
using BLL.DTOS;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserServices
    {
        public static List<UserDTO> Get()
        {
            var data = DataAcessFactory.UserData().Read();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<UserDTO>>(data);
            return mapped;
        }
        public static UserDTO GetById(string id)
        {
            var data = DataAcessFactory.UserData().Read(id);
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<UserDTO>(data);
            return mapped;
        }
        public static bool Create(UserDTO userDTO)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<UserDTO, User>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<User>(userDTO);

            var data = DataAcessFactory.UserData().Create(mapped);
            if (data != null) return true;
            return false;
        }

        public static bool Delete(string id)
        {
            bool operation = DataAcessFactory.UserData().Delete(id);
            return operation;
        }

        public static UserDTO Update(UserDTO UserDTO)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<UserDTO, User>();
            });
            var mapper = new Mapper(cfg);
            User User = mapper.Map<User>(UserDTO);

            var data = DataAcessFactory.UserData().Update(User);
            return mapper.Map<UserDTO>(data);
        }

        /*
        public static UserDTO GetByEmailAndPassword(string email, string password)
        {
            var data = DataAcessFactory.UserData().GetByEmailAndPassword(email, password);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<UserDTO>(data);
            return mapped;
        }
        */


    }
}
