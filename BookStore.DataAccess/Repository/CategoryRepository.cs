using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;

namespace BookStore.DataAccess.Repository
{
    /// <summary>
    /// extends repository and ICategory but repository is already defined so only
    /// ICategory needs to be implemented
    /// </summary>
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// uses the constuctor of base(Repository)
        /// </summary>
        /// <param name="context"></param>
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category obj)
        {
            _context.Categories.Update(obj);
        }
    }
}
