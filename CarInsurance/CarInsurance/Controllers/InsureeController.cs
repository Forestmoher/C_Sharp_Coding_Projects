using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities1 db = new InsuranceEntities1();

        // GET: Insuree
        public ActionResult Index()
        {
            var insurees = db.Insurees.Include(i => i.Coverage);
            return View(insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            ViewBag.CoverageType = new SelectList(db.Coverages, "CoverageId", "CoverageType");
            return View();
        }

        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CoverageType = new SelectList(db.Coverages, "CoverageId", "CoverageType", insuree.CoverageType);
            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoverageType = new SelectList(db.Coverages, "CoverageId", "CoverageType", insuree.CoverageType);
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoverageType = new SelectList(db.Coverages, "CoverageId", "CoverageType", insuree.CoverageType);
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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

        public decimal GetQuote(Insuree insuree)
        { 
            int quote = 50;
            int age = Convert.ToInt32(DateTime.Now - Convert.ToDateTime(insuree.DateOfBirth));
            int carYear = Convert.ToInt32(insuree.CarYear);
            string carMake = insuree.CarMake.ToString().ToLower();
            string carModel = insuree.CarModel.ToString().ToLower();
            int tickets = Convert.ToInt32(insuree.SpeedingTickets);
            bool dui = Convert.ToBoolean(insuree.DUI);
            string coverage = insuree.Coverage.ToString().ToLower();
            if (age < 18) quote += 100;
            else if (age > 18 && age < 25) quote += 50;
            else quote += 25;     
            if (carYear < 2000) quote += 25;          
            else if (carYear > 2015) quote += 25;      
            if (carMake == "porche") quote += 25;
            if (carMake == "porche" && carModel == "911 carrera") quote += 25;
            for (int i = 0; i < tickets; i++)
            {
                quote += 10;
            }
            if (dui == true) quote += (quote / 4);
            if (coverage == "full") quote += (quote / 2);

            insuree.Quote = quote;

            return insuree.Quote;


        }
    }
}
