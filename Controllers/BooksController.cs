using Books_MVC_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Books_MVC_Project.Controllers
{
    public class BooksController : Controller
    {
        private readonly IToastNotification _toastNotification;
        Context context = new Context();

        public BooksController(IToastNotification toastNotification)
        {
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
            List<Book> list = context.Books.Include(b => b.Category).ToList();
            return View(list);
        }

        public IActionResult Add()
        {
            Book book = new Book();
            List<Category> categories = context.Categories.Where(c => c.isActive)
                                                          .OrderBy(c => c.Name)
                                                          .ToList();
            ViewBag.Categories = categories;
            return View(book);
        }

        [HttpPost]
        public IActionResult Save(Book newBook)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = context.Categories.Where(c => c.isActive).ToList();
                ViewBag.Categories = categories;
                return View("Add", newBook);
            }

            if (newBook.Id == 0)
            {
                _toastNotification.AddSuccessToastMessage("The book was Added successfully");
                context.Books.Add(newBook);
            }
            else
            {
                Book existingBook = context.Books.Find(newBook.Id);
                if (existingBook == null)
                {
                    return NotFound();
                }

                existingBook.Name = newBook.Name;
                existingBook.Author = newBook.Author;
                existingBook.Category_id = newBook.Category_id;
                existingBook.Description = newBook.Description;
                _toastNotification.AddSuccessToastMessage("The book was updated successfully");
            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
                return BadRequest();

            else if (!context.Books.Any(b => b.Id == id))
                return NotFound();
            Book book = context.Books.Find(id);
            List<Category> categories = context.Categories.Where(c => c.isActive).ToList();
            ViewBag.Categories = categories;
            return View("Add",book);
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
                return BadRequest();
            Book bookToDelete = context.Books.Find(id);
            if (bookToDelete == null)
                return NotFound();
            context.Books.Remove(bookToDelete);
            context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("The book was deleted successfully");
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            if(id == null)
                return BadRequest();

            else if(!context.Books.Any(b => b.Id == id))
                return NotFound();
            
            Book book = context.Books.Where(b=>b.Id == id).Include(b => b.Category).FirstOrDefault();
            return View(book);
        }

    }
}
