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
    public class AuthServices
    {
        public static TokenDTO Authenticate(string email, string pass)
        {

            var res = DataAcessFactory.AuthData().Authenticate(email, pass);
            if (res)
            {
                var token = new Token();
                token.UserId = email;
                token.CreatedAt = DateTime.Now;
                token.TKey = Guid.NewGuid().ToString();
                var ret = DataAcessFactory.TokenData().Create(token);
                if (ret != null)
                {
                    var cfg = new MapperConfiguration(c => {
                        c.CreateMap<Token, TokenDTO>();
                    });
                    var mapper = new Mapper(cfg);
                    return mapper.Map<TokenDTO>(ret);
                }

            }
            return null;
        }
        public static bool IsTokenValid(string tkey)
        {
            var extk = DataAcessFactory.TokenData().Read(tkey);
            if (extk != null && extk.DeletedAt == null)
            {
                return true;
            }
            return false;
        }
        public static bool Logout(string tkey)
        {
            var extk = DataAcessFactory.TokenData().Read(tkey);
            extk.DeletedAt = DateTime.Now;
            if (DataAcessFactory.TokenData().Update(extk) != null)
            {
                return true;
            }
            return false;


        }
        public static bool IsAdmin(string tkey)
        {
            var extk = DataAcessFactory.TokenData().Read(tkey);
            if (IsTokenValid(tkey) && extk.User.Type.Equals("Admin"))
            {
                return true;
            }
            return false;
        }

    }
}
