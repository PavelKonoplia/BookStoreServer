using System;
using System.Linq;
using System.Linq.Expressions;

namespace BookStore.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        T FindBy(Expression<Func<T, bool>> predicate);

        void Add(T item);

        void Update(T item);

        void Delete(int id);
    }
}
