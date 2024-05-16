using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOS
{
    public class RecipeCommentDTO
    {
        public List<CommentDTO> Comments { get; set; }
        public RecipeCommentDTO()
        {
            Comments = new List<CommentDTO>();
        }
    }
}
