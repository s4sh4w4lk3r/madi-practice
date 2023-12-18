namespace Repository.Models
{
    public class FileRepo
    {
        public int Id { get; set; }
        public required string Filename { get; set; }
        public required string Path { get; set; }
        public required string MimeType { get; set; }
    }
}
