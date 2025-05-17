using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using U_LearningNET.Models;

namespace U_LearningNET.Controllers
{
    public class InstructorsController : Controller
    {
        private collegeSystemEntities1 db = new collegeSystemEntities1();

        // GET: Instructors
        public ActionResult Index()
        {
            var instructors = db.Instructors.Include(i => i.Users);
            return View(instructors.ToList());
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructors instructors = db.Instructors.Find(id);
            if (instructors == null)
            {
                return HttpNotFound();
            }
            return View(instructors);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            ViewBag.instructor_id = new SelectList(
                db.Users.Select(u => new {
                    user_id = u.user_id,
                    DisplayText = u.user_id + " - " + u.first_name + " " + u.last_name
                }),
                "user_id",
                "DisplayText"
            );
            return View();
        }


        // POST: Instructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "instructor_id,bio")] Instructors instructors)
        {
            if (ModelState.IsValid)
            {
                db.Instructors.Add(instructors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.instructor_id = new SelectList(
                db.Users.Select(u => new {
                    user_id = u.user_id,
                    DisplayText = u.user_id + " - " + u.first_name + " " + u.last_name
                }),
                "user_id",
                "DisplayText",
                instructors.instructor_id
            );
            return View(instructors);
        }


        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructors instructors = db.Instructors.Find(id);
            if (instructors == null)
            {
                return HttpNotFound();
            }
            ViewBag.instructor_id = new SelectList(
                           db.Users.Select(u => new {
                               user_id = u.user_id,
                               DisplayText = u.user_id + " - " + u.first_name + " " + u.last_name
                           }),
                           "user_id",
                           "DisplayText",
                           instructors.instructor_id
                       ); return View(instructors);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "instructor_id,bio")] Instructors instructors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.instructor_id = new SelectList(
                           db.Users.Select(u => new {
                               user_id = u.user_id,
                               DisplayText = u.user_id + " - " + u.first_name + " " + u.last_name
                           }),
                           "user_id",
                           "DisplayText",
                           instructors.instructor_id
                       ); return View(instructors);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructors instructors = db.Instructors.Find(id);
            if (instructors == null)
            {
                return HttpNotFound();
            }
            return View(instructors);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructors instructors = db.Instructors.Find(id);
            db.Instructors.Remove(instructors);
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
