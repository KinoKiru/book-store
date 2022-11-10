using BookStore.DataAccess.Repository.IRepository;

namespace BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// unit of work can be reused
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
