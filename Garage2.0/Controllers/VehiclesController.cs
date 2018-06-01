using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2._0.Models;

namespace Garage2._0.Controllers
{
    public class VehiclesController : Controller
    {
        private Garage2_0Context db = new Garage2_0Context();

        // GET: Vehicles
        public ActionResult Index(string option, string search) {
            if (option == "RegNum") {
                return View(db.Vehicles.Where(e => e.RegNum.ToLower() == search.ToLower() || search == null).ToList());
            } else if (option == "VehicleType") {
                return View(db.Vehicles.Where(e => e.VehicleType.ToString().ToLower() == search.ToLower() || search == null).ToList());
            } else {
                return View(db.Vehicles.Where(e => e.Color.ToString().ToLower() == search.ToLower() || search.ToLower() == null).ToList());
            }
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null) {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create() {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VehicleType,RegNum,Color,NumOfTires,Model")] Vehicle vehicle) {
            vehicle.CheckInTime = DateTime.Now;
            if (ModelState.IsValid) {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null) {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleType,RegNum,Color,CheckInTime,NumOfTires,Model")] Vehicle vehicle) {
            if (ModelState.IsValid) {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null) {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            // Actual delete is made in Receipt in order to avoid an enormous GET query string,
            // which could be manipulated by user
            return RedirectToAction("Receipt", new { id });
        }

        // GET: Vehicles/Receipt/5
        public ActionResult Receipt(int id) {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            var model = new ReceiptViewModel(vehicle);
            return View(model);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
