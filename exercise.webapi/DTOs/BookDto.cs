namespace exercise.webapi.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // todo refactor to list of authors
        public int AuthorId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
    }
}
