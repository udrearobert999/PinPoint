using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
