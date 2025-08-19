using exercise.webapi.Factories;
using exercise.webapi.Repository;

namespace exercise.webapi.Endpoints
{
    public static class AuthorApi
    {
        public static void ConfigureAuthorsApi(this WebApplication app)
        {
            var authors = app.MapGroup("authors");

            authors.MapGet("/", GetAuthors);
            authors.MapGet("/{id}", GetAuthor);

            private static async Task<IResult> GetAuthors(IBookRepository bookRepository)
        {
            var books = await bookRepository.GetAllBooks();

            var dtos = books.Select(BookFactory.BookDtoFromBook).ToList();
            return TypedResults.Ok(dtos);
        }

        private static async Task<IResult> GetAuthor(IBookRepository bookRepository, int id)
        {
            var book = await bookRepository.GetBook(id);

            if (book is null)
            {
                return TypedResults.NotFound();
            }

            var dto = BookFactory.BookDtoFromBook(book);
            return TypedResults.Ok(book);
        }
    }
    }
}
