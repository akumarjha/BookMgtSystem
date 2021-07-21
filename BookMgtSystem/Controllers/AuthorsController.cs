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
    public class AuthorsController : Controller
    {
        private SystemEntities db = new SystemEntities();

        //
        // GET: /Authors/

        public ActionResult Index()
        {
            return View(db.Authors.ToList());
        }

        //
        // GET: /Authors/Details/5

        public ActionResult Details(int id = 0)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            var results = from b in db.Books
                          select new
                          {
                              b.BookId,
                              b.Title,
                              Checked = ((from ab in db.AuthorBook
                                          where (ab.AuthorId == id) && (ab.BookId == b.BookId)
                                          select ab).Count() > 0)
                          };
            var myViewModel = new AuthorsViewModel();
            myViewModel.AuthorId = id;
            myViewModel.FirstName = author.FirstName;
            myViewModel.LastName = author.LastName;
            myViewModel.DateOfBirth = author.DateOfBirth;

            var myCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in results)
            {
                myCheckBoxList.Add(new CheckBoxViewModel { Id = item.BookId, Name = item.Title, Checked = item.Checked });
            }
            myViewModel.Books = myCheckBoxList;
            return View(myViewModel);
        }

        //
        // GET: /Authors/Create

        public ActionResult Create()
        {
            var results = from b in db.Books
                          select new
                          {
                              b.BookId,
                              b.Title
                          };

            var myViewModel = new AuthorsViewModel();
            var myCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in results)
            {
                myCheckBoxList.Add(new CheckBoxViewModel { Id = item.BookId, Name = item.Title });
            }
            myViewModel.Books = myCheckBoxList;
            return View(myViewModel);
        }

        //
        // POST: /Authors/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorsViewModel authorVM)
        {
            if (ModelState.IsValid)
            {
                var myAuthor = new Author();
                myAuthor.FirstName = authorVM.FirstName;
                myAuthor.LastName = authorVM.LastName;
                myAuthor.DateOfBirth = authorVM.DateOfBirth;
                db.Entry(myAuthor).State = EntityState.Added;

                foreach (var item in authorVM.Books)
                {
                    if (item.Checked)
                    {
                        db.AuthorBook.Add(new AuthorBook()
                        {
                            AuthorId = authorVM.AuthorId,
                            BookId = item.Id
                        });
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(authorVM);
        }

        //
        // GET: /Authors/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            var results = from b in db.Books
                          select new
                          {
                              b.BookId,
                              b.Title,
                              Checked = ((from ab in db.AuthorBook
                                          where (ab.AuthorId == id) && (ab.BookId == b.BookId)
                                          select ab).Count() > 0)
                          };
            var myViewModel = new AuthorsViewModel();
            myViewModel.AuthorId = id;
            myViewModel.FirstName = author.FirstName;
            myViewModel.LastName = author.LastName;
            myViewModel.DateOfBirth = author.DateOfBirth;

            var myCheckBoxList = new List<CheckBoxViewModel>();
            foreach (var item in results)
            {
                myCheckBoxList.Add(new CheckBoxViewModel { Id = item.BookId, Name = item.Title, Checked = item.Checked });
            }
            myViewModel.Books = myCheckBoxList;
            return View(myViewModel);
        }

        //
        // POST: /Authors/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorsViewModel authorVM)
        {
            if (ModelState.IsValid)
            {
                var myAuthor = db.Authors.Find(authorVM.AuthorId);
                myAuthor.FirstName = authorVM.FirstName;
                myAuthor.LastName = authorVM.LastName;
                myAuthor.DateOfBirth = authorVM.DateOfBirth;
                foreach (var item in db.AuthorBook)
                {
                    if (item.AuthorId == authorVM.AuthorId)
                    {
                        db.Entry(item).State = EntityState.Deleted;
                    }
                }
                foreach (var item in authorVM.Books)
                {
                    if (item.Checked)
                    {
                        db.AuthorBook.Add(new AuthorBook()
                        {
                            AuthorId = authorVM.AuthorId,
                            BookId = item.Id
                        });
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(authorVM);
        }

        //
        // GET: /Authors/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        //
        // POST: /Authors/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Author author = db.Authors.Find(id);
            foreach (var item in db.AuthorBook)
            {
                if(item.AuthorId == id)
                {
                    db.Entry(item).State = EntityState.Deleted;
                }
            }
            //db.Entry(author.AuthorsBooks).State = EntityState.Deleted;
            db.Authors.Remove(author);
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