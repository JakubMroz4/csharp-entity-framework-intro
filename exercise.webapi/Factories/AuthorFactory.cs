using exercise.webapi.DTOs;
using exercise.webapi.Models;

namespace exercise.webapi.Factories
{
    public static class AuthorFactory
    {
        public static AuthorDto AuthorDtoFromAuthor(Author author)
        {
            var dto = new AuthorDto();

            dto.Id = author.Id;
            dto.FirstName = author.FirstName;
            dto.LastName = author.LastName;
            dto.Email = author.Email;

            var bookDtos = new List<BookDto>();
            foreach (var book in author.Books) 
            { 
                bookDtos.Add(BookFactory.BookDtoFromBook(book));
            }

            dto.Books = bookDtos;

            return dto;
        }
    }
}
