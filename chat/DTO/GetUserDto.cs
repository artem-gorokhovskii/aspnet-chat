using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.DTO
{
    public class GetUserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
