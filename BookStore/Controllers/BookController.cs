using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository<Book> _bookRepository;
        private IBookRepository<Author> _authorRepos;

        public BookController(IBookRepository<Book> bookRepository , IBookRepository<Author> authorRepos)
        {
            _bookRepository = bookRepository;
            _authorRepos = authorRepos;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var model = _bookRepository.List();
            return View(model);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = _bookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                BookId = _bookRepository.GetComputedId(),
                Authors = _authorRepos.List().ToList()
            };
            return View(model);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            try
            {
                Book b = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = _authorRepos.Find(model.AuthorId)
                };
                _bookRepository.Add(b);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _bookRepository.Find(id);
            var athId = book.Author == null ? book.Author.Id=0 : book.Author.Id;
            var model = new BookAuthorViewModel
            {
                BookId = book.Id,
                AuthorId = athId,
                Title = book.Title,
                Description = book.Description,
                Authors = _authorRepos.List().ToList()
            };
            return View(model);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,BookAuthorViewModel model)
        {
           
            try
            {
                var author = _authorRepos.Find(model.AuthorId);
                Book book = new Book
                {
                    Title = model.Title,
                    Description = model.Description,
                    Author = author
                };
                _bookRepository.Update(model.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = _bookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
