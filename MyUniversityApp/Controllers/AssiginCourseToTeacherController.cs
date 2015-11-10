using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyUniversityApp.Models;

namespace MyUniversityApp.Controllers
{
    public class AssiginCourseToTeacherController : Controller
    {
        private UniversityDBcontext db = new UniversityDBcontext();

        // GET: /AssiginCourseToTeacher/
        public ActionResult Index()
        {
            var assigincoursetoteachers = db.AssiginCourseToTeachers.Include(a => a.Course).Include(a => a.Department).Include(a => a.Teacher);
            return View(assigincoursetoteachers.ToList());
        }

        // GET: /AssiginCourseToTeacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssiginCourseToTeacher assigincoursetoteacher = db.AssiginCourseToTeachers.Find(id);
            if (assigincoursetoteacher == null)
            {
                return HttpNotFound();
            }
            return View(assigincoursetoteacher);
        }

        // GET: /AssiginCourseToTeacher/Create
        public ActionResult Create()
        {

            ViewBag.DepartmentId = new SelectList("", "", "");
            ViewBag.CourseId = new SelectList("", "", "");
            ViewBag.TeacherId = new SelectList("", "", "");
            return View();
        }


        [HttpPost]
        public JsonResult GetAllDepartment()
        {
            var result = new SelectList(db.Departments.OrderBy(d => d.DepartmentCode), "DepartmentId", "DepartmentCode");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTeacherByDepartmentId(int departmentId)
        {
            var teachers = db.Teachers.Where(t => t.DepartmentId == departmentId);

            var result = new SelectList(teachers.OrderBy(t => t.TeacherName), "TeacherId", "TeacherName");
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetCourseByDepartmentId(int departmentId)
        {
            var course = db.Courses.Where(c => db.AssiginCourseToTeachers.FirstOrDefault(d=>d.CourseId == c.CourseId).CourseId != c.CourseId && c.DepartmentId == departmentId);
            var result = new SelectList(course, "CourseId", "CourseCode");
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTeacherInfo(int teacherId)
        {
            Teacher teacher = db.Teachers.Find(teacherId);
            Teacher aTeacher = new Teacher
            {
                TeacherCraditLimit = teacher.TeacherCraditLimit,
                RemainingCradit = teacher.RemainingCradit
            };
            return Json(aTeacher, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCourseInfo(int courseId)
        {
            var course = db.Courses.Find(courseId);
            Course aCourse = new Course
            {
                CourseName = course.CourseName,
                Cradit = course.Cradit
            };
            return Json(aCourse, JsonRequestBehavior.AllowGet);
        }

        // POST: /AssiginCourseToTeacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssiginCourseToTeacherId,DepartmentId,CourseId,TeacherId")] AssiginCourseToTeacher assigincoursetoteacher)
        {
            if (ModelState.IsValid)
            {
                Session.RemoveAll();
                var targetedTeacher = db.Teachers.Find(assigincoursetoteacher.TeacherId);
                var targetedCourse = db.Courses.Find(assigincoursetoteacher.CourseId);
                if (targetedTeacher.RemainingCradit < targetedCourse.Cradit)
                {
                    Session["course"] = targetedCourse;
                    Session["teacher"] = targetedTeacher;
                    Session["assign"] = assigincoursetoteacher;
                    return RedirectToAction("AskToAsign");
                }
                targetedTeacher.AssignedCradit = targetedTeacher.AssignedCradit + targetedCourse.Cradit;
                targetedTeacher.RemainingCradit = targetedTeacher.TeacherCraditLimit - targetedTeacher.AssignedCradit;
                db.Teachers.AddOrUpdate(targetedTeacher);
                db.SaveChanges();
                db.AssiginCourseToTeachers.Add(assigincoursetoteacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList("", "", "");
            ViewBag.DepartmentId = new SelectList("", "", "");
            ViewBag.TeacherId = new SelectList("", "", "");
            return View();
        }

        // GET: /AssiginCourseToTeacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssiginCourseToTeacher assigincoursetoteacher = db.AssiginCourseToTeachers.Find(id);
            if (assigincoursetoteacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", assigincoursetoteacher.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", assigincoursetoteacher.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", assigincoursetoteacher.TeacherId);
            return View(assigincoursetoteacher);
        }

        // POST: /AssiginCourseToTeacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssiginCourseToTeacherId,DepartmentId,CourseId,TeacherId")] AssiginCourseToTeacher assigincoursetoteacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assigincoursetoteacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", assigincoursetoteacher.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", assigincoursetoteacher.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", assigincoursetoteacher.TeacherId);
            return View(assigincoursetoteacher);
        }
        
        // GET: /AssiginCourseToTeacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssiginCourseToTeacher assigincoursetoteacher = db.AssiginCourseToTeachers.Find(id);
            if (assigincoursetoteacher == null)
            {
                return HttpNotFound();
            }
            return View(assigincoursetoteacher);
        }
        [Authorize(Roles = "Admin")]
        // POST: /AssiginCourseToTeacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssiginCourseToTeacher assigincoursetoteacher = db.AssiginCourseToTeachers.Find(id);
            db.AssiginCourseToTeachers.Remove(assigincoursetoteacher);
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
        [Authorize(Roles = "Admin")]
        public ActionResult AskToAsign()
        {
            if (Session["course"] != null)
            {
           
            Course aCourse = (Course) Session["course"];
            Teacher aTeacher = (Teacher) Session["teacher"];
            AssiginCourseToTeacher assigin = (AssiginCourseToTeacher) Session["assign"];

            if (aTeacher.RemainingCradit < 0)
            {
                ViewBag.message = ViewBag.Message = aTeacher.TeacherName
                + " can take no more course of credit : " + aCourse.Cradit
                + " \n ! Do you still want to assign this course to this teacher ?";
            }
            else
            {
                ViewBag.message = aTeacher.TeacherName
               + " has only " + aTeacher.RemainingCradit
               + " credits remaining ." + "\n" + "your Selected Course : " + aCourse.CourseCode
               + " has " + aCourse.Cradit + " Credits"
               + "\n  ! Do you still want to assign this course to this teacher ?";
            }
                return View();
            }
            return RedirectToAction("Create");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AssignConfirmed()
        {
            if (Session["course"] != null)
            {
                Course aCourse = (Course) Session["course"];
                Teacher aTeacher = (Teacher) Session["teacher"];
                AssiginCourseToTeacher assigin = (AssiginCourseToTeacher) Session["assign"];
                aTeacher.AssignedCradit = aTeacher.AssignedCradit + aCourse.Cradit;
                aTeacher.RemainingCradit = aTeacher.TeacherCraditLimit - aTeacher.AssignedCradit;
                db.Teachers.AddOrUpdate(aTeacher);
                db.SaveChanges();
                db.AssiginCourseToTeachers.Add(assigin);
                db.SaveChanges();
                Session.RemoveAll();
                TempData["success"] = "Course Assigned Successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        [HttpPost]
        public JsonResult GetAssignInfo(int departmentId)
        {
            var assignInfo =
                db.AssiginCourseToTeachers.Include(a => a.Department)
                    .Include(a => a.Course)
                    .Include(a => a.Teacher)
                    .Where(a => a.DepartmentId == departmentId);
            var listedAssign = assignInfo.ToList();

            List<AssiginCourseToTeacher> assiginCourse = new List<AssiginCourseToTeacher>();
            foreach (AssiginCourseToTeacher assignResult in listedAssign)
            {
                Semister aSemister = new Semister();
                Course aCourse = new Course();
                Teacher aTeacher = new Teacher();
                AssiginCourseToTeacher assigin = new AssiginCourseToTeacher();
                //Semister semister =
                //    db.Semisters.FirstOrDefault(
                //        s => db.Courses.Find(assignResult.CourseId).SemisterId == s.SemisterId);
                aSemister.SemisterName = assignResult.Course.Semister.SemisterName;
                aCourse.CourseCode = assignResult.Course.CourseCode;
                aCourse.CourseName = assignResult.Course.CourseName;
                aCourse.Semister = aSemister;
                assigin.Course = aCourse;
                aTeacher.TeacherName = assignResult.Teacher.TeacherName;
                assigin.Teacher = aTeacher;
                assiginCourse.Add(assigin);
            }
            return Json(assiginCourse.OrderBy(a => a.Course.CourseCode), JsonRequestBehavior.AllowGet);
        }
    }
}
