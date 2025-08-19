using exercise.webapi.Data;
using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        DataContext _db;

        public AuthorRepository(DataContext db)
        {
            _db = db;
        }

        public Task<IEnumerable<Author>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<Author?> GetAuthor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
