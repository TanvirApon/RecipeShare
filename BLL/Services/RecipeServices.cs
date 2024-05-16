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
    
    public class RecipeServices
    {
        
        public static List<RecipeDTO> Get()
        {
            var data = DataAcessFactory.RecipeData().Read();
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<Recipe, RecipeDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<RecipeDTO>>(data);
            return mapped;

        }
        public static RecipeDTO Get(int id)
        {
            var data = DataAcessFactory.RecipeData().Read(id);
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<Recipe, RecipeDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<RecipeDTO>(data);
            return mapped;

        }
        public static RecipeCommentDTO GetwithComments(int id)
        {
            var data = DataAcessFactory.RecipeData().Read(id);
            var cfg = new MapperConfiguration(c => {
                c.CreateMap<Recipe, RecipeCommentDTO>();
                c.CreateMap<Comment, CommentDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<RecipeCommentDTO>(data);
            return mapped;

        }
        public static bool DeleteRecipe(int id)
        {
            var res = DataAcessFactory.RecipeData().Delete(id);
            return res;
        }

        public static bool CreateRecipe(RecipeDTO recipeDTO)
        {

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<RecipeDTO, Recipe>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Recipe>(recipeDTO);

            var data = DataAcessFactory.RecipeData().Create(mapped);
            return data;
        }

     

        public static RecipeDTO UpdateRecipe(RecipeDTO recipeDTO)
        {

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<RecipeDTO, Recipe>();
                //c.CreateMap<Recipe, RecipeDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Recipe>(recipeDTO);

            var data = DataAcessFactory.RecipeData().Update(mapped);
            return mapper.Map<RecipeDTO>(data);
            
        }
    }
}
