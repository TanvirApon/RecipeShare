using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string CommentText { get; set; }
        public DateTime Time { get; set; }
        [ForeignKey("User")]
        public string CommentedBy { get; set; }
        [ForeignKey("Recipe")]
        public int? RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public Comment()
        {
            Users = new List<User>();
        }
    }
}
