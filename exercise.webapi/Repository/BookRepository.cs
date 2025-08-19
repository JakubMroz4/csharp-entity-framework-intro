using exercise.webapi.Data;
using exercise.webapi.Exceptions;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class BookRepository: IBookRepository
    {
        DataContext _db;
        
        public BookRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<Book?> CreateBook(Book book)
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> DeleteBook(int id)
        {
            var entity = _db.Books.FirstOrDefault(x => x.Id == id);

            if (entity is null)
            {
                return entity;
            }

            _db.Books.Remove(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Author).ToListAsync();

        }

        public async Task<Book?> GetBook(int id)
        {
            return await _db.Books.Where(b => b.Id == id).Include(b => b.Author).FirstOrDefaultAsync();
        }

        public async Task<Book?> UpdateBook(int id, Book book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();

            return book;
        }
    }
}
