﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat.Dto
{
    public class CreateUserDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
