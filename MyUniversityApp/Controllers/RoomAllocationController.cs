using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyUniversityApp.Models;

namespace MyUniversityApp.Controllers
{
    public class RoomAllocationController : Controller
    {
        private UniversityDBcontext db = new UniversityDBcontext();

        // GET: /RoomAllocation/
        public ActionResult Index()
        {
            var roomallocations = db.RoomAllocations.Include(r => r.Course).Include(r => r.Day).Include(r => r.Department).Include(r => r.Room);
            return View(roomallocations.ToList());
        }

        // GET: /RoomAllocation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            if (roomallocation == null)
            {
                return HttpNotFound();
            }
            return View(roomallocation);
        }

        // GET: /RoomAllocation/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomCode");
            return View();
        }

        // POST: /RoomAllocation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomAllocationId,DepartmentId,CourseId,RoomId,DayId,FromTime,ToTime")] RoomAllocation roomallocation)
        {
            if (ModelState.IsValid)
            {
                string schedule = "Room No:" + roomallocation.Room.RoomCode + ", " + roomallocation.Day.DayName + ", " +
                                     roomallocation.FromTime+" - "  + roomallocation.ToTime + ";";
                roomallocation.Schedule = schedule;
                db.RoomAllocations.Add(roomallocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", roomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", roomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", roomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomCode", roomallocation.RoomId);
            return View(roomallocation);
        }

        // GET: /RoomAllocation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            if (roomallocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", roomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", roomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", roomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomCode", roomallocation.RoomId);
            return View(roomallocation);
        }

        // POST: /RoomAllocation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RoomAllocationId,DepartmentId,CourseId,RoomId,DayId,Schedule")] RoomAllocation roomallocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomallocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", roomallocation.CourseId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", roomallocation.DayId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", roomallocation.DepartmentId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomCode", roomallocation.RoomId);
            return View(roomallocation);
        }

        // GET: /RoomAllocation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            if (roomallocation == null)
            {
                return HttpNotFound();
            }
            return View(roomallocation);
        }

        // POST: /RoomAllocation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomAllocation roomallocation = db.RoomAllocations.Find(id);
            db.RoomAllocations.Remove(roomallocation);
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
