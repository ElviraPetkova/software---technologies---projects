using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        public IActionResult Index()
        {
            using (var db = new LibraryDbContext())
            {
                var allBooks = db.Books.ToList();

                return View(allBooks);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(string title, string author, double price)
        {
            if(string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || price < 1)
            {
                return RedirectToAction("Index");
            }

            using (var db = new LibraryDbContext())
            {
                Book book = new Book()
                {
                    Title = title,
                    Author = author,
                    Price = price
                };

                db.Books.Add(book);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var db = new LibraryDbContext())
            {
                var bookToEdit = db.Books.FirstOrDefault(x => x.Id == id);
                if(bookToEdit == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(bookToEdit);
            }
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            using (var db = new LibraryDbContext())
            {
                var bookToEdit = db.Books.FirstOrDefault(x => x.Id == book.Id);
                bookToEdit.Title = book.Title;
                bookToEdit.Author = book.Author;
                bookToEdit.Price = book.Price;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var db = new LibraryDbContext())
            {
                var bookToDelete = db.Books.FirstOrDefault(x => x.Id == id);
                if (bookToDelete == null)
                {
                    return RedirectToAction("Index");
                }

                return this.View(bookToDelete);
            }
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            using (var db = new LibraryDbContext())
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}