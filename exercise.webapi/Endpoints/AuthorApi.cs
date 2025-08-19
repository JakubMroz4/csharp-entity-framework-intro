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
        }

        private static async Task<IResult> GetAuthors(IAuthorRepository authorRepository)
        {
            var authors = await authorRepository.GetAllAuthors();

            var dtos = authors.Select(AuthorFactory.AuthorDtoFromAuthor).ToList();
            return TypedResults.Ok(dtos);
        }

        private static async Task<IResult> GetAuthor(IAuthorRepository authorRepository, int id)
        {
            var author = await authorRepository.GetAuthor(id);

            if (author is null)
            {
                return TypedResults.NotFound();
            }

            var dto = AuthorFactory.AuthorDtoFromAuthor(author);
            return TypedResults.Ok(dto);
        }
    }
}
