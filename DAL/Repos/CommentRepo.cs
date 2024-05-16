using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class CommentRepo : Repo, IRepo<Comment, int, bool>
    {
        public bool Create(Comment obj)
        {
            db.Comments.Add(obj);
            if (db.SaveChanges() > 0) return true;
            return false;
        }

        public bool Delete(int id)
        {
            var expost = db.Comments.Find(id);
            db.Comments.Remove(expost);
            return (db.SaveChanges() > 0);
        }

        public List<Comment> Read()
        {
            return db.Comments.ToList();
        }

        public Comment Read(int id)
        {
            return db.Comments.Find(id);
        }

        public bool Update(Comment obj)
        {
            var ex = Read(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            if (db.SaveChanges() > 0) return true; else return false;
        }
    }
}
