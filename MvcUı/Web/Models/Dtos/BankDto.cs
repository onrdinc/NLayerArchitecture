namespace Web.Models.Dtos
{
    public class BankDto
    {
        public class Form 
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
        public class  FilterForm
        {
            public string? Search { get; set; }

        }
        public class Response
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }


    }
}
