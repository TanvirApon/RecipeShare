using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOS
{
    public class UserCommentDTO
    {
        public List<UserDTO> Users { get; set; }
        public UserCommentDTO()
        {
            Users = new List<UserDTO>();
        }
    }
}
