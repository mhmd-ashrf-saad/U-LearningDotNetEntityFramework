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
    public class CoursesController : Controller
    {
        private collegeSystemEntities1 db = new collegeSystemEntities1();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Instructors);
            return View(courses.ToList());

        }

        //GET: Latest Courses
        public ActionResult Latest()
        {
            
            var latestCourses = db.Courses
     .Include(c => c.Instructors.Users)
     .OrderByDescending(c => c.course_id)
     .Take(3)
     .ToList();

            return PartialView("_LatestCourses", latestCourses);
        }



        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            
            ViewBag.instructor_id = new SelectList(
                  db.Users.Select(u => new {
                      user_id = u.user_id,
                      DisplayText = u.user_id + " - " + u.first_name + " " + u.last_name
                  }),
                  "user_id",
                  "DisplayText"
              ); return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "course_id,title,description,instructor_id")] Courses courses, HttpPostedFileBase PhotoFile)
        {
            if (PhotoFile != null && PhotoFile.ContentLength > 0)
            {
                // Generate a unique file name
                var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(PhotoFile.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Images/Courses/"), fileName);
                PhotoFile.SaveAs(path);
                // Save the relative path to the database
                courses.image_url = "~/Images/Courses/" + fileName;
            }

            if (ModelState.IsValid)
            {
                db.Courses.Add(courses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // ... your ViewBag code ...
            return View(courses);
        }


        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            ViewBag.instructor_id = new SelectList(
                  db.Users.Select(u => new {
                      user_id = u.user_id,
                      DisplayText = u.user_id + " - " + u.first_name + " " + u.last_name
                  }),
                  "user_id",
                  "DisplayText"
              ); return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "course_id,title,description,instructor_id,image_url")] Courses courses, HttpPostedFileBase PhotoFile)
        {
            if (PhotoFile != null && PhotoFile.ContentLength > 0)
            {
                var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(PhotoFile.FileName);
                var path = System.IO.Path.Combine(Server.MapPath("~/Images/Courses/"), fileName);
                PhotoFile.SaveAs(path);
                courses.image_url = "~/Images/Courses/" + fileName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // ... your ViewBag code ...
            return View(courses);
        }


        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Courses courses = db.Courses.Find(id);
            db.Courses.Remove(courses);
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
