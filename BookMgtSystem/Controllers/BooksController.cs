using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookMgtSystem.Models;

namespace BookMgtSystem.Controllers
{
    public class BooksController : Controller
    {
        private SystemEntities db = new SystemEntities();

        //
        // GET: /Books/

        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        //
        // GET: /Books/Details/5

        public ActionResult Details(int id = 0)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // GET: /Books/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Books/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {      
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(book);
        }

        public ActionResult CreateBook()
        {
            return PartialView("_Create");
        }

        //
        // POST: /Books/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateBook(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();

                return PartialView("_Books");
            }

            return View(book);
        }
        //
        // GET: /Books/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // POST: /Books/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        //
        // GET: /Books/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // POST: /Books/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}