using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class BillsController : Controller
    {
        private SnackShopDBContext db = new SnackShopDBContext();
        private BillDao Bdao = new BillDao();

        // GET: Admin/Bills
        public ActionResult Index()
        {
            var bills = db.Bills.Include(b => b.BillStatu).Include(b => b.Customer);
            return View(bills.ToList());
        }

        // GET: Admin/Bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Admin/Bills/Create
        public ActionResult Create()
        {
            ViewBag.id_status = new SelectList(db.BillStatus, "id_status", "status");
            ViewBag.id_customer = new SelectList(db.Customers, "id_customer", "name");
            return View();
        }

        // POST: Admin/Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_bill,subtotal,total,creatDate,id_customer,discountCode,discount,address,phone,id_status")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_status = new SelectList(db.BillStatus, "id_status", "status", bill.id_status);
            ViewBag.id_customer = new SelectList(db.Customers, "id_customer", "name", bill.id_customer);
            return View(bill);
        }

        // GET: Admin/Bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_status = new SelectList(db.BillStatus, "id_status", "status", bill.id_status);
            ViewBag.id_customer = new SelectList(db.Customers, "id_customer", "name", bill.id_customer);
            return View(bill);
        }

        // POST: Admin/Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_bill,subtotal,total,creatDate,id_customer,discountCode,discount,address,phone,id_status")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_status = new SelectList(db.BillStatus, "id_status", "status", bill.id_status);
            ViewBag.id_customer = new SelectList(db.Customers, "id_customer", "name", bill.id_customer);
            return View(bill);
        }

        // GET: Admin/Bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Admin/Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
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

        public ActionResult IndexBillDetail(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var BillDetail = Bdao.BillDetail(id);
            if(BillDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_bill = id;

            return PartialView(BillDetail);


        }


    }
}
