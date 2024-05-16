using DAL.Interfaces;
using DAL.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAcessFactory
    {
        public static IRepo<Recipe, int, bool> RecipeData()
        {
            return new RecipeRepo();
        }
        public static IRepo<Comment, int, bool> CommentData()
        {
            return new CommentRepo();
        }
        public static IRepo<User,string,User> UserData()
        {
            return new UserRepo();
        }

        public static IAuth<bool> AuthData()
        {
            return new UserRepo();
        }
        public static IRepo<Token, string, Token> TokenData()
        {
            return new TokenRepo();
        }

    }
    }

