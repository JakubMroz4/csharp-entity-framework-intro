using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        DataContext _db;

        public PublisherRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishers()
        {
            return await _db.Publishers
                .Include(p => p.Books)
                .ThenInclude(b => b.Author)
                .ToListAsync();
        }

        public async Task<Publisher?> GetPublisher(int id)
        {
            var entity = _db.Publishers
                .Where(p => p.Id == id)
                .Include(a => a.Books)
                .ThenInclude(b => b.Author)
                .FirstOrDefaultAsync();

            if (entity is null)
            {
                return null;
            }

            return await entity;
        }
    }
}
