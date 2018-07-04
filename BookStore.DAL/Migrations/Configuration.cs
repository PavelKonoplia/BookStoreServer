namespace BookStore.DAL.Migrations
{
    using BookStore.Entity.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookStore.DAL.IdentityDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookStore.DAL.IdentityDatabaseContext context)
        {
            Tag t1 = new Tag { Id = 1, TagName = "Book" };
            Tag t2 = new Tag { Id = 2, TagName = "BookNew" };
            Tag t3 = new Tag { Id = 3, TagName = "Life" };
            Tag t4 = new Tag { Id = 4, TagName = "Dream" };
            Tag t5 = new Tag { Id = 5, TagName = "Read" };
            Tag t6 = new Tag { Id = 6, TagName = "Brain" };
            Tag t7 = new Tag { Id = 7, TagName = "BetterNow" };

            Book b1 = new Book { Id = 1, Price = 112.20m, Title = "NewBook", Author = "Henry Candle", Description = "Book about life", ISO = "00241290", Genre = "Roman", Tags = { t2, t5, t4, t3 } };
            Book b2 = new Book { Price = 68m, Title = "Me", Author = "O Den", Description = "Its about me", ISO = "00241290", Genre = "Biography", Tags = { t5, t4, t3 } };
            Book b3 = new Book { Price = 118m, Title = "You", Author = "O Den", Description = "Its about you", ISO = "42144215", Genre = "Roman", Tags = { t1, t6, t4, t3 } };
            Book b4 = new Book { Price = 210.50m, Title = "War and peace", Author = "Lev Tolstoy", Description = "About war and peace", ISO = "00241290", Genre = "Roman", Tags = { t6, t7 } };
            Book b5 = new Book { Price = 60m, Title = "Lion", Author = "Marky Sand", Description = "Wild life", ISO = "21521521", Genre = "Nature", Tags = { t6, t7 } };
            Book b6 = new Book { Price = 37.60m, Title = "ZooLand", Author = "Tim Tomson", Description = "Good book", ISO = "00241290", Genre = "Poem", Tags = { t6, t7 } };

            User u1 = new User { Id = 1, Email = "mymail@gmail.com", UserName = "Admin", PasswordHash = "Admin" };
            User u2 = new User { Id = 2, Email = "mymail2@gmail.com", UserName = "Sam", PasswordHash = "qwerty" };
            User u3 = new User { Id = 3, Email = "mymail3@gmail.com", UserName = "Tom", PasswordHash = "qwerty", Selling = { b1, b2 } };
            User u4 = new User { Id = 4, Email = "mymail4@gmail.com", UserName = "Ban", PasswordHash = "qwerty" };
            User u5 = new User { Id = 5, Email = "mymail5@gmail.com", UserName = "Jack", PasswordHash = "qwerty", Selling = { b5, b6 } };

            Order o1 = new Order { Id = 1, User = u2, Goods = { b1, b2, b5 } };
            Order o2 = new Order { Id = 2, User = u4, Goods = { b2, b5 } };

            try
            {
                
                context.Books.Add(b1);
                context.Books.Add(b2);
                context.Books.Add(b3);
                context.Books.Add(b4);
                context.Books.Add(b5);
                context.Books.Add(b6);

                context.Tags.Add(t1);
                context.Tags.Add(t2);
                context.Tags.Add(t3);
                context.Tags.Add(t4);
                context.Tags.Add(t5);
                context.Tags.Add(t6);
                context.Tags.Add(t7);

                context.Users.Add(u1);
                context.Users.Add(u2);
                context.Users.Add(u3);
                context.Users.Add(u4);
                context.Users.Add(u5);

                context.Orders.Add(o1);
                context.Orders.Add(o2);

                context.SaveChanges();
            }

            catch
            {
                throw;
            }

        }
    }
}