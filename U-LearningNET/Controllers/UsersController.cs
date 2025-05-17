using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using U_LearningNET.Models;
using System.Web.Security;
namespace U_LearningNET.Controllers
{
    public class UsersController : Controller
    {


        public ActionResult Login()
        {
            return View();
        } 
        
        public ActionResult Register()
        {
            return View();
        }

        //Login Authorizattion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["ErrorMessage"] = "اسم المستخدم وكلمة المرور مطلوبان.";
                return View();
            }

            var user = db.Users.FirstOrDefault(u => u.username == username && u.password == password);
            if (user == null)
            {
                TempData["ErrorMessage"] = "اسم المستخدم أو كلمة المرور غير صحيحة.";
                return View();
            }

            FormsAuthentication.SetAuthCookie(username, false);
            return RedirectToAction("Index", "Home");
        }



        private collegeSystemEntities1 db = new collegeSystemEntities1();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Instructors).Include(u => u.Staff).Include(u => u.Students);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(db.Instructors, "instructor_id", "bio");
            ViewBag.user_id = new SelectList(db.Staff, "staff_id", "position");
            ViewBag.user_id = new SelectList(db.Students, "student_id", "student_id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,first_name,username,last_name,phone,age,profile,photo,password,role")] Users users, HttpPostedFileBase PhotoFile)
        {
            if (PhotoFile != null && PhotoFile.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(PhotoFile.InputStream))
                {
                    users.photo = reader.ReadBytes(PhotoFile.ContentLength);
                }
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.user_id = new SelectList(db.Instructors, "instructor_id", "bio", users.user_id);
            ViewBag.user_id = new SelectList(db.Staff, "staff_id", "position", users.user_id);
            ViewBag.user_id = new SelectList(db.Students, "student_id", "student_id", users.user_id);
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(db.Instructors, "instructor_id", "bio", users.user_id);
            ViewBag.user_id = new SelectList(db.Staff, "staff_id", "position", users.user_id);
            ViewBag.user_id = new SelectList(db.Students, "student_id", "student_id", users.user_id);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,first_name,last_name,username,phone,age,profile,photo,password,role")] Users users, HttpPostedFileBase PhotoFile)
        {

            if (PhotoFile != null && PhotoFile.ContentLength > 0)
            {
                using (var reader = new System.IO.BinaryReader(PhotoFile.InputStream))
                {
                    users.photo = reader.ReadBytes(PhotoFile.ContentLength);
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.user_id = new SelectList(db.Instructors, "instructor_id", "bio", users.user_id);
            ViewBag.user_id = new SelectList(db.Staff, "staff_id", "position", users.user_id);
            ViewBag.user_id = new SelectList(db.Students, "student_id", "student_id", users.user_id);
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
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
