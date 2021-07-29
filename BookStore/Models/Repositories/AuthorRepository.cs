using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class AuthorRepository : IBookRepository<Author>
    {
        IList<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>() {
                new Author{Id=1, FullName="Author 001"},
                new Author{Id=2, FullName="Author 002"},
                new Author{Id=3, FullName="Author 003"},
                new Author{Id=4, FullName="Author 004"},
                new Author{Id=5, FullName="Author 005"},
                new Author{Id=6, FullName="Author 006"},
            };
        }
        public void Add(Author entity)
        {
            // Generate ID from Code 
            entity.Id = authors.Max(b => b.Id) + 1; ;
            authors.Add(entity);
        }
        public void Delete(int id)
        {
            var author = Find(id);
            authors.Remove(author);
        }

        public Author Find(int id)
        {
            var author = authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int id, Author entity)
        {
            var author = Find(id);
            author.FullName = entity.FullName;
        }
    }
}


