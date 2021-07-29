using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookDbRepository : IBookRepository<Book>
    {
        BookStoreDbContext db;
        public BookDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }
        
        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(b => b.Author).ToList();
        }

        public List<Book> Search(string term)
        {
            var result = db.Books.Include(b => b.Author).Where(b => b.Title.Contains(term) || b.Description.Contains(term)
            || b.Author.FullName.Contains(term)).ToList();
            return result;
        }

        public void Update(int id, Book entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }
    }

}
