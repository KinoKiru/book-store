namespace BookStore.DataAccess.Repository.IRepository
{
    /// <summary>
    /// save will be used for every class this is a reusable class
    /// </summary>
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        void Save();
    }
}
