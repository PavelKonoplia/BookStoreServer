using BookStore.Common.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace BookStore.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext userDataContext;
        private DbSet<T> dbSet;

        public Repository(DbContext dataContext)
        {
            this.userDataContext = dataContext;
            this.dbSet = userDataContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return this.dbSet.AsNoTracking();
        }

        public T FindBy(Expression<Func<T, bool>> predicate)
        {
            return this.dbSet.Where(predicate).FirstOrDefault();
        }

        public void Delete(int id)
        {
            T t = this.dbSet.Find(id);
            this.dbSet.Remove(t);
            this.userDataContext.SaveChanges();
        }

        public void Update(T t)
        {
            this.userDataContext.Entry(t).State = EntityState.Modified;
            this.userDataContext.SaveChanges();
        }

        public virtual void Add(T entity)
        {
            this.dbSet.Add(entity);
            this.userDataContext.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userDataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
