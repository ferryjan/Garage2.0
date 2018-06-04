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
        /*
         *       Truck: 2 parking spaces
         *         Van: 1 parking space
         *         Car: 1 parking space
         *  Motorcycle: 1/3 parking space
         */

        public static readonly int ParkingCapacity = 10;
        private Garage2_0Context db = new Garage2_0Context();

        // GET: Vehicles
        public ActionResult Index(string option, string search) {
            ViewBag.AvailableSpaces = _AvailableSpacesCount();
            ViewBag.Capacity = ParkingCapacity;
            if (ViewBag.AvailableSpaces == 0) {
                if (!_HasSpaceForMotorCycle()) {
                    ViewBag.Msg = "There are no parking spaces available, please come later!";
                } else {
                    ViewBag.Msg = "There are no parking space for car/van/truck. However, we still have space for motorcycle. Welcome!";
                }
            } else {
                ViewBag.Msg = "<h3>Welcome! You can park your vehicle here! <br />Car/Van: 1 parking space, 5 SEK/15min <br />Truck: 2 parking spaces, 10 SEK/15min" +
                    "<br />Motorcycle: 3 motorcycles can share same parking space, 5 SEK/15min</h3>";
            }
            if (option == "RegNum") {
                return View(db.Vehicles.Where(e => e.RegNum.ToLower() == search.ToLower() || search == null).ToList());
            } else if (option == "VehicleType") {
                return View(db.Vehicles.Where(e => e.VehicleType.ToString().ToLower() == search.ToLower() || search == null).ToList());
            } else {
                return View(db.Vehicles.Where(e => e.Color.ToString().ToLower() == search.ToLower() || search.ToLower() == null).ToList());
            }
        }

        private bool _HasSpaceForMotorCycle() => db.ParkingSpaces.Count(p => (p.Vehicles.Count == 0 || (p.Vehicles.Count < 3 && p.Vehicles.FirstOrDefault().VehicleType == VehicleTypes.Motorcycle))) > 0;

        private int _AvailableSpacesCount() => db.ParkingSpaces.Where(p => p.Vehicles.Count == 0).Count();


        // GET: Vehicles/Details/5
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null) {
                return HttpNotFound();
            }
            ViewBag.ParkingPosition = String.Join(" and ", vehicle.ParkingSpaces.Select(p => p.Number.ToString()).ToArray());
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
        public ActionResult Create([Bind(Include = "VehicleType,RegNum,Color,NumOfTires,Model")] Vehicle vehicle) {
            vehicle.CheckInTime = DateTime.Now;
            bool parked = _AssignParkingSpace(vehicle);
            if (ModelState.IsValid) {
                if (parked) {
                    ViewBag.isFull = "";
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } else {
                    ViewBag.isFull = "There is no place to park your vehicle, sorry!";
                }
            }
            return View(vehicle);
        }

        private bool _AssignParkingSpace(Vehicle vehicle) {
            foreach (ParkingSpace ps in db.ParkingSpaces.OrderBy(p => p.Number)) {
                if (vehicle.VehicleType == VehicleTypes.Motorcycle && (ps.Vehicles.Count == 0 || (ps.Vehicles.Count < 3 && ps.Vehicles.FirstOrDefault()?.VehicleType == VehicleTypes.Motorcycle))) {
                    vehicle.ParkingSpaces.Add(ps);
                    return true;
                } else if (ps.Vehicles.Count == 0) {
                    if (vehicle.VehicleType == VehicleTypes.Truck) {
                        ParkingSpace nextSpace = db.ParkingSpaces.Where(p => p.Number == ps.Number + 1 && p.Vehicles.Count == 0).SingleOrDefault();
                        if (nextSpace != null) {
                            vehicle.ParkingSpaces.Add(ps);
                            vehicle.ParkingSpaces.Add(nextSpace);
                            return true;
                        }
                    } else {
                        vehicle.ParkingSpaces.Add(ps);
                        return true;
                    }
                }
            }
            return false;
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
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicle = db.Vehicles.Find(id);
            if (TryUpdateModel(vehicle, "", new string[] { "RegNum", "Color", "NumOfTires", "Model" })) {
                try {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                } catch (DataException /* dex */) {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
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
            ViewBag.ParkingPosition = String.Join(" and ", vehicle.ParkingSpaces.Select(p => p.Number.ToString()).ToArray());
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

        // GET: /Statistics/
        public ActionResult Statistics() {
            var vehicles = db.Vehicles.ToList();
            var model = new StatisticsViewModel(vehicles);
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
