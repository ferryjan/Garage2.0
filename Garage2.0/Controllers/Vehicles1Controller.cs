﻿using System;
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
        private Garage2_0Context db = new Garage2_0Context();

        // GET: Vehicles1
        public ActionResult Index()
        {
            var vehicles = db.Vehicles.Include(v => v.Member).Include(v => v.VehicleType);
            return View(vehicles.ToList());
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
            return View(vehicle);
        }

        // GET: Vehicles1/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name");
            ViewBag.TypeId = new SelectList(db.VehicleTypes, "TypeId", "Type");
            return View();
        }

        // POST: Vehicles1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RegNum,Color,NumOfTires,Model,ParkingSpaceNum,TypeId,MemberId")] Vehicle vehicle)
        {
            vehicle.CheckInTime = DateTime.Now;
            if (ModelState.IsValid)
            {
                
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", vehicle.MemberId);
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
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", vehicle.MemberId);
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
            ViewBag.MemberId = new SelectList(db.Members, "MemberId", "Name", vehicle.MemberId);
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
            //if (vehicle.VehicleType == VehicleTypes.Truck)
            //{
            //    ViewBag.ParkingPosition = (vehicle.ParkingSpaceNum + 1) + " and " + (vehicle.ParkingSpaceNum + 2);
            //}
            //else
            //{
            //    ViewBag.ParkingPosition = vehicle.ParkingSpaceNum + 1;
            //}
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
            // ParkingSpace ps = new ParkingSpace(parkingCapacity);
            // ps.RemoveFromParkingSpace(vehicle);
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
