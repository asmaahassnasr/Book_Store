using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository<Book> _bookRepository;
        private IBookRepository<Author> _authorRepos;
        private IWebHostEnvironment _hostingEnvironment;

        public BookController(IBookRepository<Book> bookRepository , IBookRepository<Author> authorRepos,IWebHostEnvironment hostingEnvironment)
        {
            _bookRepository = bookRepository;
            _authorRepos = authorRepos;
            _hostingEnvironment = hostingEnvironment;

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
                BookId = _bookRepository.List().Max(b => b.Id) + 1,
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
                string fileName = string.Empty;
                if(model.File != null)
                {
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    fileName = model.File.FileName;
                    string fullPath = Path.Combine(uploads, fileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                Book b = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = _authorRepos.Find(model.AuthorId),
                    ImageUrl= fileName
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
                ImagUrl= book.ImageUrl,
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
                string fileName = string.Empty;
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    fileName = model.File.FileName;
                    string fullPath = Path.Combine(uploads, fileName);

                    //delete old file path
                    string oldFileName = _bookRepository.Find(model.BookId).ImageUrl;
                    string fullOldPath = Path.Combine(uploads, oldFileName);

                    if (fullPath != fullOldPath)
                    {
                        //Delet old
                        System.IO.File.Delete(fullOldPath);
                        //Add new File pathe
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                }
                Book book = new Book
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl=fileName,
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
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                _bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
