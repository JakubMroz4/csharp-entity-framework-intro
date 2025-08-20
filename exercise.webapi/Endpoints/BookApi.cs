using exercise.webapi.DTOs;
using exercise.webapi.Factories;
using exercise.webapi.Models;
using exercise.webapi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
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
            books.MapPost("/", CreateBook).Accepts<BookPostDto>("application/json");
            books.MapPut("/{id}", UpdateBook).Accepts<BookPutDto>("application/json");
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
            return TypedResults.Ok(dto);
        }

        private static async Task<IResult> CreateBook(IBookRepository bookRepository, HttpRequest request)
        {
            var dto = await ValidateFromRequest<BookPostDto>(request);
            
            if (dto is null)
            {
                return TypedResults.BadRequest();
            }

            var book = BookFactory.BookFromBookPost(dto);
            var created = await bookRepository.CreateBook(book);
            if (created is null)
            {
                return TypedResults.NotFound();
            }

            var outDto = BookFactory.BookDtoFromBook(created);
            return TypedResults.Ok(outDto);
        }

        private static async Task<IResult> UpdateBook(IBookRepository bookRepository, int id, HttpRequest request)
        {
            var dto = await ValidateFromRequest<BookPutDto>(request);
            if (dto is null)
            {
                return TypedResults.BadRequest();
            }

            var book = await bookRepository.GetBook(id);
            if (book is null)
            {
                return TypedResults.NotFound();
            }

            if (dto.AuthorId is not null) book.AuthorId = dto.AuthorId.Value;
            if (dto.PublisherId is not null) book.PublisherId = dto.PublisherId.Value;

            var updated = await bookRepository.UpdateBook(book);
            if (updated is null)
            {
                return TypedResults.NotFound();
            }


            var outDto = BookFactory.BookDtoFromBook(updated);

            return TypedResults.Ok(outDto);
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

        private async static Task<T> ValidateFromRequest<T>(HttpRequest request)
        {
            T? entity;
            try
            {
                entity = await request.ReadFromJsonAsync<T>();
            }
            catch (JsonException ex)
            {
                return default;
            }

            return entity;
        }
    }
}
