using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            if (customer.Age % 2 == 0)
            {
                ViewBag.RetireAge = 75;
            }
            if (customer.Age % 2 != 0)
            {
                ViewBag.RetireAge = 105;
            }
            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.VacLocation = "Huntsville, AL";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.VacLocation = "Bayou Le Batre, AL";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.VacLocation = "Spanish Fort, AL";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.VacLocation = "Birmingham, AL";
            }
            else
            {
                ViewBag.VacLocation = "a tent in Mississippi";
            }

            switch (customer.FavColor)
            {
                case "red":
                    ViewBag.Transportation = "Motorcycle";
                    break;
                case "orange":
                    ViewBag.Transportation = "Roller Blades";
                    break;
                case "yellow":
                    ViewBag.Transportation = "Banana Seat Bicycle";
                    break;
                default:
                    ViewBag.Transportaion = "your feet";
                    break;
            }
            if (customer.BirthMonth >= 1 && customer.BirthMonth <= 4)
            {
                ViewBag.RetireMoney = "$15,000,000.00";
            }
            if (customer.BirthMonth >= 5 && customer.BirthMonth <= 8)
            {
                ViewBag.RetireMoney = "$12,000,000.00";
            }
            if (customer.BirthMonth >= 9 && customer.BirthMonth <= 12)
            {
                ViewBag.RetireMoney = "$5,000,000.00";
            }

            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
