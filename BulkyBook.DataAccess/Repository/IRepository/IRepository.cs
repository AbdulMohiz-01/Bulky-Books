using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // includeProperties represent that if we want query more than one table at time. it's just a logic
        // it will be sent as Category,Cover etc
        IEnumerable<T> GetAll(string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);


    }
}
