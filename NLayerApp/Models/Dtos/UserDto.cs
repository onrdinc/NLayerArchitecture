using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class UserDto
    {

        public class Form
        {
            public long Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }

        }

        public class FilterForm 
        {

        }

        public class Response
        {
            public long Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? UserName { get; set; }
            public string? Email { get; set; }
            public string? Token { get; set; }


        }



        public class LoginForm
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

    }

}
