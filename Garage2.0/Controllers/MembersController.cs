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
    public class MembersController : Controller
    {
        private Garage2_0Context db = new Garage2_0Context();

        // GET: Members
        public ActionResult Index(string option, string search)
        {
            List<int> list = new List<int>();
            foreach (var member in db.Members)
            {
                var tempList = db.Vehicles.Where(m => m.MemberId == member.MemberId);
                list.Add(tempList.Count());
            }

            if (TempData["Msg"] == null)
            {
                ViewBag.Message = "";
            }
            else if (TempData["Msg"].ToString() == "success")
            {
                ViewBag.Message = "This member has been successfully deleted";
            }
            else
            {
                ViewBag.Message = "This member cannot be deleted due to he/she has vehicle parked in the garage! Check out the Vehicle first!";
            }

            ViewBag.TotalCarsForEachMember = list;

            if (option == "MemberId")
            {
                return View(db.Members.Where(e => e.MemberId.ToString() == search.ToLower() || search == null).ToList());
            }
            else
            {
                return View(db.Members.Where(e => e.Name.ToLower() == search.ToLower() || search == null).ToList());
            }
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberId,Name,Address,PhoneNr")] Member member)
        {
            member.RegDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberId,Name,Address,PhoneNr,RegDate")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            var tempList = db.Vehicles.Where(m => m.MemberId == member.MemberId);
            if (tempList.Count() == 0)
            {
                ViewBag.UnableToDeleteMsg = "";
                return View(member);
            }
            else
            {
                TempData["Msg"] = "failed";
                return RedirectToAction("Index");
            }
            
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            TempData["Msg"] = "success";
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
