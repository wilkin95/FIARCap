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

namespace FIARCap.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer")]
        public ActionResult Index()
        {
            return View(BuildBookReviewViewModelList(db.Reviews.ToList()));
            return View(db.Books.OrderBy(b => b.Title).ToList());
        }

        //GET: user create book review
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer, Book Admin, User")]
        public ActionResult UserCreate()
        {
            return View();
        }

        //POST: uer create book review
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer, Book Admin, User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate([Bind(Include = "Id, DateCreated, Content, BookID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("ListOfReviewsByBook", new { id = review.BookID });
            }

            return View(review);
        }

        //list of reviews for a given Book
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer, Book Admin, User")]
        public ActionResult ListOfReviewsByBook(int Id)
        {
            var bookReviews = db.Reviews
                .Where(r => r.BookID == Id)
                .ToList();

            //get book to pass
            var book = db.Books.FirstOrDefault(b => b.Id == Id);
            ViewBag.Book = book;

            if (book != null)
            {
                return View(bookReviews);
            }
            else
            {
                //redirect to error page 
                ViewBag.ErrorMessage = "Book not found.";
                return View("Error");
            }
        }


        [NonAction]
        private BookReviewViewModel BuildBookReviewViewModel(Review review)
        {
            //generate a dictionary with book ids and names for lookup
            var bookTitles = db.Books.ToDictionary(b => b.Id, b => b.Title);
            return new BookReviewViewModel()
            {
                ID = review.ID,
                DateCreated = review.DateCreated,
                Content = review.Content,
                BookID = review.BookID,
                BookTitle = bookTitles[review.BookID]
            };

        }

        [NonAction]
        private List<BookReviewViewModel> BuildBookReviewViewModelList(List<Review> reviews)
        {
            List<BookReviewViewModel> bookReviewViewModel = new List<BookReviewViewModel>();

            //generate a dictionary with book ids and names for lookup
            var bookTitles = db.Books.ToDictionary(b => b.Id, b => b.Title);

            foreach (var review in reviews)
            {
                bookReviewViewModel.Add(new BookReviewViewModel
                {
                    ID = review.ID,
                    DateCreated = review.DateCreated,
                    Content = review.Content,
                    BookID = review.BookID,
                    BookTitle = bookTitles[review.BookID]
                });
            }
            return bookReviewViewModel.OrderByDescending(b => b.DateCreated).ToList() ;
        }


        // GET: Reviews/Details/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer, Book Admin, User")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            BookReviewViewModel bookReviewViewModel = BuildBookReviewViewModel(review);
            
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(bookReviewViewModel);
        }

        // GET: Reviews/Create
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer, Book Admin, User")]
        public ActionResult Create()
        {
            //generate select list with ids for book dropdown
            var bookList = db.Books.Select(b => b);
            ViewBag.SelectBookList = new SelectList(bookList, "Id", "Title");

            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer, Book Admin, User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DateCreated,Content,BookID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(review);
        }

        // GET: Reviews/Edit/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Review review = db.Reviews.Find(id);
            BookReviewViewModel bookReviewViewModel = BuildBookReviewViewModel(review);
            //generate select list with ids for brewery dropdown
            var bookList = db.Books.Select(b => b);
            ViewBag.SelectBookList = new SelectList(bookList, "Id", "Title");
           

            if (review == null)
            {
                return HttpNotFound();
            }
            return View(bookReviewViewModel);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DateCreated,Content,BookID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            BookReviewViewModel bookReviewViewModel = BuildBookReviewViewModel(review);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(bookReviewViewModel);
        }

        // POST: Reviews/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin, Reviewer")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
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
