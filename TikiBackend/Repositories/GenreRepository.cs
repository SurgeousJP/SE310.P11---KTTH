using BookCatalog.API.Infrastructure;
using BookCatalog.API.Models;

namespace BookCatalog.API.Repositories
{
    public class GenreRepository : GenericRepository<Genre>
    {
        public GenreRepository(BookContext context) : base(context)
        {
        }
    }
}
