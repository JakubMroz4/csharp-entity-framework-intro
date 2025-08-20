using exercise.webapi.DTOs;
using exercise.webapi.Models;

namespace exercise.webapi.Factories
{
    public static class PublisherFactory
    {
        public static PublisherDto DtoFromPublisher(Publisher publisher)
        {
            var dto = new PublisherDto();
            dto.Id = publisher.Id;
            dto.Name = publisher.Name;

            List<BookDto> bookDtos = new();
            foreach (var book in publisher.Books)
            {
                bookDtos.Add(BookFactory.BookDtoFromBook(book));
            }

            dto.Books = bookDtos;

            return dto;
        }
    }
}
