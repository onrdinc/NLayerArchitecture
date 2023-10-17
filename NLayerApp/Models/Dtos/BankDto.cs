using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class BankDto
    {
        public class Form 
        {
            public int Id { get; set; }

            public string Name { get; set; }

        }
        public class FilterForm 
        {

        }
        public class Response 
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
