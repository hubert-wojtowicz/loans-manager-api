using System;
using System.Collections.Generic;
using System.Text;

namespace LoansManager.Services.Dtos
{
    public class CreateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
