using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookRepository : IBookRepository<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>() {
            new Book{Id=1, Title="Book 001" , Description="This is book 001" , Author= new Author{ Id=1} , ImageUrl="1.jpg"} ,
            new Book{Id=2, Title="Book 002" , Description="This is book 002" , Author= new Author{ Id=1}  , ImageUrl="3.jpg"} ,
            new Book{Id=3, Title="Book 003" , Description="This is book 003" , Author= new Author{ Id=3}  , ImageUrl="4.jpg"} ,
            new Book{Id=4, Title="Book 004" , Description="This is book 004", Author= new Author{ Id=2}  , ImageUrl="5.jpg"} ,
            new Book{Id=5, Title="Book 005" , Description="This is book 005" , Author= new Author{ Id=3}  , ImageUrl="6.jpg"} ,
            new Book{Id=6, Title="Book 006" , Description="This is book 006" , Author= new Author{ Id=3}  , ImageUrl="8.jpg"}
            };
        }
        public void Add(Book entity)
        {
            books.Add(entity);
        }
        public int GetComputedId()
        {
            return  books.Max(b => b.Id) + 1;
        }
        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id, Book entity)
        {
            var book = Find(id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Author = entity.Author;
            book.ImageUrl = entity.ImageUrl;
        }
    }

}
