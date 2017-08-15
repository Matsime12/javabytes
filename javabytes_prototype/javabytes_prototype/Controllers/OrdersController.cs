﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using javabytes_prototype.Models;

namespace javabytes_prototype.Controllers
{
    public class OrdersController : Controller
    {
        private javabytesDBEntities db = new javabytesDBEntities();

        //this method will need to recieve something different when actually saving orders in the db
        public ActionResult Checkout(string[] orders)
        {

            return View("Payment");    
        }

        //this method will receive an order number and check if payment has been made via snapscan
        public ActionResult Payment(int id)
        {
            //Check if payment made
            //If no - check again (5 times? before saying payment error?)
            //If yes - redirect to order status page
            return View("OrderStatus");
        }

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Order_Status).Include(o => o.User).Include(o => o.Vendor);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.order_status_id = new SelectList(db.Order_Status, "order_status_id", "name");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "name");
            ViewBag.vendor_id = new SelectList(db.Vendors, "vendor_id", "name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "order_id,date,total,delivery_collection,delivery_location,order_status_id,user_id,vendor_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.order_status_id = new SelectList(db.Order_Status, "order_status_id", "name", order.order_status_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "name", order.user_id);
            ViewBag.vendor_id = new SelectList(db.Vendors, "vendor_id", "name", order.vendor_id);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.order_status_id = new SelectList(db.Order_Status, "order_status_id", "name", order.order_status_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "name", order.user_id);
            ViewBag.vendor_id = new SelectList(db.Vendors, "vendor_id", "name", order.vendor_id);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "order_id,date,total,delivery_collection,delivery_location,order_status_id,user_id,vendor_id")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.order_status_id = new SelectList(db.Order_Status, "order_status_id", "name", order.order_status_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "name", order.user_id);
            ViewBag.vendor_id = new SelectList(db.Vendors, "vendor_id", "name", order.vendor_id);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
