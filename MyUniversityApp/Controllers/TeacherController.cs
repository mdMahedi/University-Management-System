using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyUniversityApp.Models;

namespace MyUniversityApp.Controllers
{
    public class TeacherController : Controller
    {
        private UniversityDBcontext db = new UniversityDBcontext();

        // GET: /Teacher/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetTeacherByDepartmentId(int departmentId)
        {
            var teachers = db.Teachers.Include(d => d.Department).Include(d => d.Designation).Where(d => d.DepartmentId == departmentId);
            List<Teacher> teacher = teachers.ToList();
            List<Teacher> jsonList = new List<Teacher>();
            foreach (Teacher teac in teacher)
            {
                Teacher aTeacher = new Teacher();
                Designation aDesignation = new Designation();
                Department aDepartment = new Department();
                aTeacher.TeacherId = teac.TeacherId;
                aDepartment.DepartmentCode = teac.Department.DepartmentCode;
                aDesignation.DesignationName = teac.Designation.DesignationName;
                aTeacher.Department = aDepartment;
                aTeacher.Designation = aDesignation;
                aTeacher.TeacherName = teac.TeacherName;
                aTeacher.Email = teac.Email;
                aTeacher.ContactNo = teac.ContactNo;
                jsonList.Add(aTeacher);
            }
            return jsonList.Count == 0 ? Json(0, JsonRequestBehavior.AllowGet) : new JsonResult{Data = jsonList, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        // GET: /Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: /Teacher/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            return View();
        }

        // POST: /Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,TeacherName,Email,ContactNo,Address,DesignationId,DepartmentId,TeacherCraditLimit,")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                teacher.RemainingCradit = teacher.TeacherCraditLimit;
                teacher.AssignedCradit = 0;
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }
        [Authorize(Roles = "Admin")]
        // GET: /Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }
        [Authorize(Roles = "Admin")]
        // POST: /Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,TeacherName,Email,ContactNo,Address,DesignationId,DepartmentId,TeacherCraditLimit,RemainingCradit,AssignedCradit")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", teacher.DepartmentId);
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            return View(teacher);
        }
        [Authorize(Roles = "Admin")]
        // GET: /Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }
        [Authorize(Roles = "Admin")]
        // POST: /Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
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

        [HttpPost]
        public JsonResult IsEmailUnique(string email)
        {
            var result = db.Teachers.Count(u => u.Email == email) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsContactUnique(string contactno)
        {
            var result = db.Teachers.Count(u => u.ContactNo == contactno) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
       
       

        [HttpPost]
        public JsonResult GetAllDepartment()
        {
            var departments = db.Departments.OrderBy(d => d.DepartmentName);
            return Json(new SelectList(departments, "DepartmentId", "DepartmentCode"), JsonRequestBehavior.AllowGet);
        }
    }
}
