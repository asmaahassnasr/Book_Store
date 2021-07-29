﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class AuthorDbRepository : IBookRepository<Author>
    {
        BookStoreDbContext db;
        public AuthorDbRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();
        }
       
        public void Delete(int id)
        {
            var author = Find(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author Find(int id)
        {
            var author = db.Authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public int GetComputedId()
        {
            throw new NotImplementedException();
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public void Update(int id, Author entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }
    }
}


