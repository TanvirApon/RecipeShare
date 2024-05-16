using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOS
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        public string Ingredient { get; set; }

        [Required]
        public string PostedBy { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
