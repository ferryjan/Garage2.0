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
    public class Vehicles1Controller : Controller
    {
        public static int parkingCapacity = 10;
        private Garage2_0Context db = new Garage2_0Context();
        public ParkingSpace parkspace = new ParkingSpace(parkingCapacity);

        // GET: Vehicles
        public ActionResult Index(string option, string search)
        {
            ViewBag.AvailableSpaces = parkspace.GetNumOfAvailableSpace();
            ViewBag.Capacity = parkingCapacity;
            if (ViewBag.AvailableSpaces == 0)
            {
                if (!parkspace.HasSpaceForMotorCycle())
                {
                    ViewBag.Msg = "There are no parking space available, please come later!";
                }
                else
                {
                    ViewBag.Msg = "There are no parking space for car/van/truck. However, we have still space for the motorcycle. Welcome!";
                }
            }
            else
            {
                ViewBag.Msg = "<h3>Welcome! You can park your vehicle here! <br />Car/Van: 1 parking space, 5 SEK/15min <br />Truck: 2 parking spaces, 10 SEK/15min" +
                    "<br />Motorcycle: 3 motorcycles can share same parking space, 5 SEK/15min</h3>";
            }
            if (option == "RegNum")
            {
                return View(db.Vehicles.Where(e => e.RegNum.ToLower() == search.ToLower() || search == null).ToList());
            }
            else if (option == "VehicleType")
            {
                switch (search.ToLower())
                {
                    case "car":
                        search = "1";
                        break;
                    case "van":
                        search = "2";
                        break;
                    case "truck":
                        search = "3";
                        break;
                    case "motorcycle":
                        search = "4";
                        break;
                    default:
                        break;
                }
                return View(db.Vehicles.Where(e => e.TypeId.ToString() == search.ToLower() || search == null).ToList());
            }
            else
            {
                return View(db.Vehicles.Where(e => e.Color.ToString().ToLower() == search.ToLower() || search.ToLower() == null).ToList());
            }
        }


        // GET: Vehicles/ViewInDetail
        public ActionResult ViewInDetail(string option, string search)
        {
            ViewBag.AvailableSpaces = parkspace.GetNumOfAvailableSpace();
            ViewBag.Capacity = parkingCapacity;
            if (ViewBag.AvailableSpaces == 0)
            {
                if (!parkspace.HasSpaceForMotorCycle())
                {
                    ViewBag.Msg = "There are no parking space available, please come later!";
                }
                else
                {
                    ViewBag.Msg = "There are no parking space for car/van/truck. However, we have still space for the motorcycle. Welcome!";
                }
            }
            else
            {
                ViewBag.Msg = "<h3>Welcome! You can park your vehicle here! <br />Car/Van: 1 parking space, 5 SEK/15min <br />Truck: 2 parking spaces, 10 SEK/15min" +
                    "<br />Motorcycle: 3 motorcycles can share same parking space, 5 SEK/15min</h3>";
            }
            if (option == "RegNum")
            {
                return View(db.Vehicles.Where(e => e.RegNum.ToLower() == search.ToLower() || search == null).ToList());
            }
            else if (option == "VehicleType")
            {
                switch (search.ToLower())
                {
                    case "car":
                        search = "1";
                        break;
                    case "van":
                        search = "2";
                        break;
                    case "truck":
                        search = "3";
                        break;
                    case "motorcycle":
                        search = "4";
                        break;
                    default:
                        break;
                }
                return View(db.Vehicles.Where(e => e.TypeId.ToString() == search.ToLower() || search == null).ToList());
            }
            else
            {
                return View(db.Vehicles.Where(e => e.Color.ToString().ToLower() == search.ToLower() || search.ToLower() == null).ToList());
            }
        }

        // GET: Vehicles1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            if (vehicle.TypeId == 3)
            {
                ViewBag.ParkingPosition = (vehicle.ParkingSpaceNum + 1) + " and " + (vehicle.ParkingSpaceNum + 2);
            }
            else
            {
                ViewBag.ParkingPosition = vehicle.ParkingSpaceNum + 1;
            }
            return View(vehicle);
        }

        // GET: Vehicles1/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "MembershipNr", "MembershipNr");
            ViewBag.TypeId = new SelectList(db.VehicleTypes, "TypeId", "Type");
            return View();
        }

        // POST: Vehicles1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegNum,Color,NumOfTires,Model,ParkingSpaceNum,TypeId,MembershipNr")] Vehicle vehicle)
        {
            vehicle.CheckInTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                ParkingSpace ps = new ParkingSpace(parkingCapacity);
            var index = ps.AssignParkingSpace(vehicle);
                if (index != -1)
                {
                    ViewBag.isFull = "";
                    vehicle.ParkingSpaceNum = index;
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.isFull = "There is no place to park your vehicle, sorry!";
                }
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MembershipNr", vehicle.MembershipNr);
            ViewBag.TypeId = new SelectList(db.VehicleTypes, "TypeId", "Type", vehicle.TypeId);
            return View(vehicle);
        }

        // GET: Vehicles1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MembershipNr", vehicle.MembershipNr);
            ViewBag.TypeId = new SelectList(db.VehicleTypes, "TypeId", "Type", vehicle.TypeId);
            return View(vehicle);
        }

        // POST: Vehicles1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RegNum,Color,CheckInTime,NumOfTires,Model,ParkingSpaceNum,TypeId,MemberId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "MembershipNr", vehicle.MembershipNr);
            ViewBag.TypeId = new SelectList(db.VehicleTypes, "TypeId", "Type", vehicle.TypeId);
            return View(vehicle);
        }


        // GET: Vehicles1/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            if (vehicle.TypeId == 3)
            {
                ViewBag.ParkingPosition = (vehicle.ParkingSpaceNum + 1) + " and " + (vehicle.ParkingSpaceNum + 2);
            }
            else
            {
                ViewBag.ParkingPosition = vehicle.ParkingSpaceNum + 1;
            }
            return View(vehicle);
        }

        // POST: Vehicles1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Actual delete is made in Receipt in order to avoid an enormous GET query string,
            // which could be manipulated by user
            return RedirectToAction("Receipt", new { id });
        }

        // GET: Vehicles1/Receipt/5
        public ActionResult Receipt(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            var model = new ReceiptViewModel(vehicle);
            ParkingSpace ps = new ParkingSpace(parkingCapacity);
            ps.RemoveFromParkingSpace(vehicle);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return View(model);
        }
        // GET: Vehicles1/Statistics
        public ActionResult Statistics()
        {
            var vehicles = db.Vehicles.ToList();
            var model = new StatisticsViewModel(vehicles);
            return View(model);
        }

        public ActionResult AdvancedView()
        {
            var model = new AdvancedViewModel(parkspace);
            return View(model);
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
