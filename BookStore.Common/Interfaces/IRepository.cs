using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookStore.Common.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T FindBy(Expression<Func<T, bool>> predicate);

        void Add(T item);

        void Update(T item);

        void Delete(int id);
    }
}
