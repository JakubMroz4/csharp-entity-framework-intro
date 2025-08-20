namespace exercise.webapi.DTOs
{
    public class PublisherDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    }
}
