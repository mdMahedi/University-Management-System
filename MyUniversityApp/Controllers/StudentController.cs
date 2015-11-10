using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyUniversityApp.Models;

namespace MyUniversityApp.Controllers
{
    public class StudentController : Controller
    {
        private UniversityDBcontext db = new UniversityDBcontext();

        // GET: /Student/
        public ActionResult Index(int? id)
        {
            if (id != null && id != 0)
            {
                var student = db.Students.Include(s => s.Department).Where(s => s.DepartmentId == id).OrderBy(s => s.StudentName);
                return View(student.ToList());
            }
            var students = db.Students.Include(s => s.Department).OrderBy(s => s.StudentName);
            return View(students.ToList());
        }

        // GET: /Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: /Student/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            return View();
        }

        // POST: /Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StudentId,StudentName,Email,ContactNo,EnrollmentDate,StudentAddress,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.StudentReg = GenarateStudentId(student);
                
                db.Students.Add(student);
                db.SaveChanges();
                TempData["reg"] = student.StudentName + ". RegistrationId is : " + student.StudentReg;
                ViewBag.RegId = student.StudentName + ". RegistrationId is : " + student.StudentReg;
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", student.DepartmentId);
            return View(student);
        }

        private string GenarateStudentId(Student student)
        {
            int id = db.Students.Count(
                    s => (s.DepartmentId == student.DepartmentId) && 
                         (s.EnrollmentDate.Year == student.EnrollmentDate.Year)) + 1;
            Department aDepartment = db.Departments.FirstOrDefault(d => d.DepartmentId == student.DepartmentId);
            string registrationId = aDepartment.DepartmentCode + "-" + student.EnrollmentDate.Year+"-";
            string zero = "";
            int length = 3 - id.ToString().Length;
            for (int i = 0; i < length; i++)
            {
                zero = "0" + zero;
            }
            return registrationId + zero + id;

        }

        // GET: /Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", student.DepartmentId);
            return View(student);
        }

        // POST: /Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StudentId,StudentName,Email,ContactNo,EnrollmentDate,StudentAddress,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                Student aStudent = db.Students.Find(student.StudentId);
                student.StudentReg = aStudent.StudentReg;

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", student.DepartmentId);
            return View(student);
        }

        // GET: /Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: /Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
            var result = db.Students.Count(u => u.Email == email) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsContactNoUnique(string contactno)
        {
            var result = db.Students.Count(u => u.ContactNo == contactno) == 0;
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
