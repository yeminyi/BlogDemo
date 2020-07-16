namespace BlogDemo.Infrastructure.Resources
{
    public class PostImageResource
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Location => $"/uploads/{FileName}";
    }
}
