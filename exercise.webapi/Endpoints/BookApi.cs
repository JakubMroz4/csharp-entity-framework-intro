using exercise.webapi.DTOs;
using exercise.webapi.Factories;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.webapi.Endpoints
{
    public static class BookApi
    {
        public static void ConfigureBooksApi(this WebApplication app)
        {
            var books = app.MapGroup("books");

            books.MapGet("/", GetBooks);
            books.MapGet("/{id}", GetBook);
            books.MapPost("/", CreateBook);
            books.MapPut("/{id}", UpdateBook);
            books.MapDelete("/{id}", DeleteBook);
        }

        private static async Task<IResult> GetBooks(IBookRepository bookRepository)
        {
            var books = await bookRepository.GetAllBooks();

            var dtos = books.Select(BookFactory.BookDtoFromBook).ToList();
            return TypedResults.Ok(dtos);
        }

        private static async Task<IResult> GetBook(IBookRepository bookRepository, int id)
        {
            var book = await bookRepository.GetBook(id);

            if (book is null)
            {
                return TypedResults.NotFound();
            }

            var dto = BookFactory.BookDtoFromBook(book);
            return TypedResults.Ok(book);
        }

        private static async Task<IResult> CreateBook(IBookRepository bookRepository, BookPostDto dto)
        {
            // todo check if author exists
            // if not - notfound404

            var book = BookFactory.BookFromBookPost(dto);
            var created = await bookRepository.CreateBook(book);

            return TypedResults.Ok(created);
        }

        private static async Task<IResult> UpdateBook(IBookRepository bookRepository, int id, BookPutDto dto)
        {
            var book = bookRepository.GetBook(id);
            if (book is null)
            {
                return TypedResults.NotFound();
            }

            // check if author is valid
            // else not found
        }

        private static async Task<IResult> DeleteBook(IBookRepository bookRepository, int id)
        {
            var deleted = await bookRepository.DeleteBook(id);

            if (deleted is null)
            {
                return TypedResults.NotFound();
            }

            var dto = BookFactory.BookDtoFromBook(deleted);
            return TypedResults.Ok(dto);
        }
    }
}
