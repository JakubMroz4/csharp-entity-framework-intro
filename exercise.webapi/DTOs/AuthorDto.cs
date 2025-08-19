using exercise.webapi.Models;

namespace exercise.webapi.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    }
}
