using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FIARCap.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using FIARCap.CustomAttribute;

namespace FIARCap.Controllers
{


    public class IdentityRoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IdentityRole
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin")]
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: IdentityRoles/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: IdentityRoles/Delete/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            IdentityRole identityRoleTemp = db.Roles.Find(id);
            db.Roles.Remove(identityRoleTemp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET: IdentityRoles/Edit/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: IdentityRoles/Edit/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit ([Bind(Include = "ID, Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        //GET: IdentityRoles/Create
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin")]
        public ActionResult Create()
        {
            return View();
        }

        //POST: IdentityRoles/Create
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID, Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }


        //GET: IdentityRoles/Details/5
        [AuthorizeOrRedirectAttribute(Roles = "Site Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = db.Roles.Find(id);
            if(role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        

        





    }
}