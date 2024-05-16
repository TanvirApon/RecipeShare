using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class RecipeRepo : Repo, IRepo<Recipe, int, bool>
    {
        public bool Create(Recipe obj)
        {
            db.Recipes.Add(obj);
            if (db.SaveChanges() > 0) return true;
            return false;
     
        }

        public bool Delete(int id)
        {
            var expost = db.Recipes.Find(id);
            db.Recipes.Remove(expost);
            return (db.SaveChanges() > 0);
        }

        public List<Recipe> Read()
        {
            return db.Recipes.ToList();
        }

        public Recipe Read(int id)
        {
            return db.Recipes.Find(id);
        }

        public bool Update(Recipe obj)
        {
            var ex = Read(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            if (db.SaveChanges() > 0) return true; else return false;
        }
    }
}
