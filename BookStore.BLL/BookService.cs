using BookStore.Common.Interfaces;
using BookStore.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.BLL
{
    public class BookService
    {
        private IRepository<Book> repository;

        public BookService(IRepository<Book> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Book> GetBooks()
        {
            return this.repository.GetAll();
        }

        public Book FindByTitle(string title)
        {
            return this.repository.FindBy(b => b.Title == title);
        }

        public Book FindById(long id)
        {
            return this.repository.FindBy(b => b.Id == id);
        }

        public bool AddBook(Book book)
        {
            this.repository.Add(book);

            return this.repository.FindBy(b => b.Title == book.Title) != null;
        }

        public void UpdateBook (Book book)
        {
            this.repository.Update(book);
        }

        public void DeleteBook(long id)
        {
            this.repository.Delete((int)id);
        }

        public void DeleteBooksByUser(long userId)
        {
            IEnumerable<Book> books = this.repository.FindMany(b => b.UserId == userId) as IEnumerable<Book>;
            var c = this.repository.FindBy(b => b.UserId == userId);
            foreach (var book in books)
            {
                this.repository.Delete(book.Id);
            }
        }
    }
}
