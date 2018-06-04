using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Garage2._0.Models
{
    public class ParkingSpacesController : Controller
    {
        private Garage2_0Context db = new Garage2_0Context();

        // GET: ParkingSpaces
        public ActionResult Index()
        {
            return View(db.ParkingSpaces.ToList());
        }

        public ActionResult Advanced()
        {
            return View(db.ParkingSpaces);
        }

        // GET: ParkingSpaces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingSpace parkingSpace = db.ParkingSpaces.Find(id);
            if (parkingSpace == null)
            {
                return HttpNotFound();
            }
            return View(parkingSpace);
        }

        // GET: ParkingSpaces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkingSpaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Capacity")] ParkingSpace parkingSpace)
        {
            if (ModelState.IsValid)
            {
                db.ParkingSpaces.Add(parkingSpace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(parkingSpace);
        }

        // GET: ParkingSpaces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingSpace parkingSpace = db.ParkingSpaces.Find(id);
            if (parkingSpace == null)
            {
                return HttpNotFound();
            }
            return View(parkingSpace);
        }

        // POST: ParkingSpaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Capacity")] ParkingSpace parkingSpace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parkingSpace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(parkingSpace);
        }

        // GET: ParkingSpaces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkingSpace parkingSpace = db.ParkingSpaces.Find(id);
            if (parkingSpace == null)
            {
                return HttpNotFound();
            }
            return View(parkingSpace);
        }

        // POST: ParkingSpaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParkingSpace parkingSpace = db.ParkingSpaces.Find(id);
            db.ParkingSpaces.Remove(parkingSpace);
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
