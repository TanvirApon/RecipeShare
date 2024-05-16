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
    public class CommentServices
    {
        public static List<CommentDTO> Get()
        {
            var data = DataAcessFactory.RecipeData().Read();
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<Comment, CommentDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<CommentDTO>>(data);
            return mapped;

        }
        public static CommentDTO Get(int id)
        {
            var data = DataAcessFactory.CommentData().Read(id);
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<Comment, CommentDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<CommentDTO>(data);
            return mapped;

        }
      
        public static bool DeleteComment(int id)
        {
            var res = DataAcessFactory.CommentData().Delete(id);
            return res;
        }

        public static bool CreateComment(CommentDTO commentDTO)
        {

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CommentDTO, Comment>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Comment>(commentDTO);

            var data = DataAcessFactory.CommentData().Create(mapped);
            return data;
        }



        public static CommentDTO UpdateComment(CommentDTO commentDTO)
        {

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CommentDTO, Comment>();
                //c.CreateMap<Recipe, RecipeDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Comment>(commentDTO);

            var data = DataAcessFactory.CommentData().Update(mapped);
            return mapper.Map<CommentDTO>(data);

        }

        public static UserCommentDTO GetwithUser(int id)
        {
            var data = DataAcessFactory.CommentData().Read(id);
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<User, UserCommentDTO>();
                c.CreateMap<Comment, CommentDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<UserCommentDTO>(data);
            return mapped;

        }
    }
}
