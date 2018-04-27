using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIARCap.Models;
using FIARCap.CustomAttribute;
using PagedList;

namespace FIARCap.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books    
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Book Admin, Reviewer, User")]
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var books = from b in db.Books
                          select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString));
                                        
            }

            

            switch (sortOrder)
            {
                case "Title":
                    books = books.OrderBy( b => b.Title);
                    break;
                case "Author":
                    books = books.OrderBy(b => b.Author);
                    break;                
                default:
                    books = books.OrderBy(b => b.Id);
                    break;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(books.ToPagedList(pageNumber, pageSize));
        }

        

       

        

        // List all books
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Book Admin, Reviewer, User")]
        public ActionResult ListBooks()
        {
            return View(db.Books.OrderBy(b => b.Title).ToList());
        }

      

        // GET: Books/Details/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Book Admin, Reviewer, User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Book Admin")]
        public ActionResult Create()
        {
            //generate a select list with ids for book dropdown
            var bookList = db.Books.Select(b => b);
            ViewBag.SelectBookList = new SelectList(bookList, "Id", "Title");

            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Book Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ISBN,Title,Author,Illustrator,Copyright,Category,Summary,Topics,ImagePath")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Book Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Book Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ISBN,Title,Author,Illustrator,Copyright,Category,Summary,Topics,ImagePath")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Book Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Book Admin")]
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
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
