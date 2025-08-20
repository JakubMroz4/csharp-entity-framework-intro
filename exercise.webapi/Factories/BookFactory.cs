using exercise.webapi.DTOs;
using exercise.webapi.Models;

namespace exercise.webapi.Factories
{
    public static class BookFactory
    {
        public static Book BookFromBookPost(BookPostDto dto)
        {
            var book = new Book();
            book.Title = dto.Title;
            book.AuthorId = dto.AuthorId;
            book.PublisherId = dto.PublisherId;

            return book;
        }

        public static BookDto BookDtoFromBook(Book book)
        {
            var dto = new BookDto();
            dto.Id = book.Id;
            dto.Title = book.Title;
            dto.AuthorId = book.AuthorId;
            dto.FirstName = book.Author.FirstName;
            dto.LastName = book.Author.LastName;
            dto.Email = book.Author.Email;
            dto.PublisherId = book.PublisherId;
            dto.PublisherName = book.Publisher.Name;

            return dto;
        }
    }
}
