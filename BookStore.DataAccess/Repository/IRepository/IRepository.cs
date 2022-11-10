using System.Linq.Expressions;

namespace BookStore.DataAccess.Repository.IRepository
{
    /// <summary>
    /// Uses generics where the generic needs to be a class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// returns all items based on generic type
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// add command
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// example func = c => c.Id == Id
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
