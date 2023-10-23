namespace Web.Models
{
    public class ResponseBody<T>
    {
        public int Status { get; set; } = 200;
        public List<string> StatusTexts { get; set; } = new List<string>();
        public T Item { get; set; }
        public long Count { get; set; } = 0;
        public TimeSpan WorkingTime { get; set; }
    }
}
