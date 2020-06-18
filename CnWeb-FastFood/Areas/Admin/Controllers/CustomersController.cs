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
using System.IO;


namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        private SnackShopDBContext db = new SnackShopDBContext();
        CustomerDao dao = new CustomerDao();
        // GET: Admin/Customers
        public ActionResult Index(int? page, int? PageSize) 
        {
            var customer = db.Customers.Count();
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
            };
            int pageNumber = (page ?? 1);
            int pagesize = (PageSize ?? 5);
            ViewBag.psize = pagesize;
            ViewBag.Count = customer;
            return View(dao.ListCustomerPage(pageNumber, pagesize));
        }

        // GET: Admin/Customers/Details/5
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
            return View(customer);
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            ViewBag.id_discountCode = new SelectList(db.DiscountCodes, "id_discountCode", "id_discountCode");
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]       
        public ActionResult Create([Bind(Include = "id_customer,name,phone,address,userName,password,id_discountCode")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                dao.Add(customer);               
                return RedirectToAction("Index");
            }

            ViewBag.id_discountCode = new SelectList(db.DiscountCodes, "id_discountCode", "id_discountCode", customer.id_discountCode);
            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
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
            ViewBag.id_discountCode = new SelectList(db.DiscountCodes, "id_discountCode", "id_discountCode", customer.id_discountCode);
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_customer,name,phone,address,userName,password,subtotalCart,totalCart,id_discountCode")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_discountCode = new SelectList(db.DiscountCodes, "id_discountCode", "id_discountCode", customer.id_discountCode);
            return View(customer);
        }

        // GET: Admin/Customers/Delete/5
        
        [HttpDelete]
        public ActionResult Delete(int? id)
        {
            CustomerDao dao = new CustomerDao();
            dao.Delete(id);
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
