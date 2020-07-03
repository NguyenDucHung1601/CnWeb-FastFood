//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using CnWeb_FastFood.Models.Dao.Admin;
//using CnWeb_FastFood.Models.EF;

//namespace CnWeb_FastFood.Areas.Admin.Controllers
//{
//    public class BillController : Controller
//    {
//        private SnackShopDBContext db = new SnackShopDBContext();
//        private BillDao Bdao = new BillDao();

//        // GET: Admin/Bills
//        public ActionResult Index()
//        {
//            var bills = db.Bills.Include(b => b.BillStatu).Include(b => b.Customer);
//            return View(bills.ToList());
//        }

//        // GET: Admin/Bills/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Bill bill = db.Bills.Find(id);
//            if (bill == null)
//            {
//                return HttpNotFound();
//            }
//            return View(bill);
//        }

//        // GET: Admin/Bills/Create
//        public ActionResult Create()
//        {
//            ViewBag.id_status = new SelectList(db.BillStatus, "id_status", "status");
//            ViewBag.id_customer = new SelectList(db.Customers, "id_customer", "name");
//            return View();
//        }

//        // POST: Admin/Bills/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "id_bill,subtotal,total,creatDate,id_customer,discountCode,discount,address,phone,id_status")] Bill bill)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Bills.Add(bill);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.id_status = new SelectList(db.BillStatus, "id_status", "status", bill.id_status);
//            ViewBag.id_customer = new SelectList(db.Customers, "id_customer", "name", bill.id_customer);
//            return View(bill);
//        }

//        // GET: Admin/Bills/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Bill bill = db.Bills.Find(id);
//            if (bill == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.id_status = new SelectList(db.BillStatus, "id_status", "status", bill.id_status);
//            ViewBag.id_customer = new SelectList(db.Customers, "id_customer", "name", bill.id_customer);
//            return View(bill);
//        }

//        // POST: Admin/Bills/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "id_bill,subtotal,total,creatDate,id_customer,discountCode,discount,address,phone,id_status")] Bill bill)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(bill).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.id_status = new SelectList(db.BillStatus, "id_status", "status", bill.id_status);
//            ViewBag.id_customer = new SelectList(db.Customers, "id_customer", "name", bill.id_customer);
//            return View(bill);
//        }

//        // GET: Admin/Bills/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Bill bill = db.Bills.Find(id);
//            if (bill == null)
//            {
//                return HttpNotFound();
//            }
//            return View(bill);
//        }

//        // POST: Admin/Bills/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Bill bill = db.Bills.Find(id);
//            db.Bills.Remove(bill);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//public ActionResult IndexBillDetail(int? id)
//{
//    if (id == null)
//    {
//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//    }
//    var BillDetail = Bdao.BillDetail(id);
//    if (BillDetail == null)
//    {
//        return HttpNotFound();
//    }
//    ViewBag.id_bill = id;

//    return PartialView(BillDetail);


//}


//    }
//}

using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Text;
using System.Net;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class BillController : Controller
    {
        SnackShopDBContext db = new SnackShopDBContext();
        BillDao Bdao = new BillDao();
        BillStatusDao BSdao = new BillStatusDao();
        BillDetailDao BDdao = new BillDetailDao();

        // GET: Admin/Product

        public ActionResult Index(int? page, int? PageSize, string searching = "")
        {
            ViewBag.SearchString = searching;
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" }
            };
            int pageNumber = (page ?? 1);
            int pagesize = (PageSize ?? 10);
            ViewBag.psize = pagesize;
            ViewBag.Count = Bdao.ListSimple(searching).Count();
            return View(Bdao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Index2(int? page, int? PageSize, string idBill, string customerName, string phone, string address, string discountCode, string discountFrom, string discountTo, string subtotalFrom, string subtotalTo, string totalFrom, string totalTo, string status)
        {
            ViewBag.IdBill = idBill;
            ViewBag.CustomerName = customerName;
            ViewBag.CustomerPhone = phone;
            ViewBag.CustomerAddress = address;
            ViewBag.DiscountCode = discountCode;
            ViewBag.DiscountFrom = discountFrom;
            ViewBag.DiscountTo = discountTo;
            ViewBag.SubtotalFrom = subtotalFrom;
            ViewBag.SubtotalTo = subtotalTo;
            ViewBag.TotalFrom = totalFrom;
            ViewBag.TotalTo = totalTo;
            ViewBag.IdBillStatus = status;
            if (status != "" && status != null)
                ViewBag.Status = BSdao.getByID(Convert.ToInt32(status)).status;

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="20", Text= "20" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" }
            };
            int pageNumber = (page ?? 1);
            int pagesize = (PageSize ?? 10);
            ViewBag.psize = pagesize;
            ViewBag.Count = Bdao.ListAdvanced(idBill, customerName, phone, address, discountCode, discountFrom, discountTo, subtotalFrom, subtotalTo, totalFrom, totalTo, status).Count();
            return View(Bdao.ListAdvancedSearch(pageNumber, pagesize, idBill, customerName, phone, address, discountCode, discountFrom, discountTo, subtotalFrom, subtotalTo, totalFrom, totalTo, status));
        }

        public ActionResult Filter()
        {
            List<BillStatus> ctgr = BSdao.ListBillStatus();
            SelectList billstatusList = new SelectList(ctgr, "id_status", "status", "id_status");
            ViewBag.BillStatusList = billstatusList;

            return View();
        }

        public ActionResult Details(int id)
        {
            Bill bill = Bdao.getByID(id);
            return View(bill);
        }

        public ActionResult IndexBillDetail(int id)
        {
            var listBillDetail = BDdao.getListBillDetailByIdBill(id);

            if (listBillDetail == null)
                return HttpNotFound();

            ViewBag.id_bill = id;

            return PartialView(listBillDetail);
        }

        //public ActionResult Create()
        //{
        //    List<Category> category = Cdao.ListCategory();
        //    SelectList categoryList = new SelectList(category, "id_category", "name", "id_category");
        //    ViewBag.CategoryList = categoryList;

        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Create(string name, string description, string information, string review, string availability, string price, string salePercent, string salePrice, string rate, HttpPostedFileBase mainPhoto, HttpPostedFileBase photo1, HttpPostedFileBase photo2, HttpPostedFileBase photo3, HttpPostedFileBase photo4, string updated, string id_category)
        //{
        //    Product product = new Product();
        //    product.name = name;
        //    product.description = description;
        //    product.information = information;
        //    product.review = review;
        //    product.availability = Convert.ToBoolean(availability);
        //    product.price = Convert.ToDecimal(price);
        //    product.salePercent = Convert.ToInt32(salePercent);
        //    product.salePrice = Convert.ToDecimal(salePrice);
        //    product.rate = float.Parse(rate);
        //    product.updated = DateTime.Parse(updated);
        //    product.id_category = Convert.ToInt32(id_category);

        //    if (ModelState.IsValid)
        //    {
        //        if (mainPhoto != null && mainPhoto.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(mainPhoto.FileName));
        //            mainPhoto.SaveAs(path);
        //            product.mainPhoto = mainPhoto.FileName;
        //        }
        //        if (photo1 != null && photo1.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo1.FileName));
        //            photo1.SaveAs(path);
        //            product.photo1 = photo1.FileName;
        //        }
        //        if (photo2 != null && photo2.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo2.FileName));
        //            photo2.SaveAs(path);
        //            product.photo2 = photo2.FileName;
        //        }
        //        if (photo3 != null && photo3.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo3.FileName));
        //            photo3.SaveAs(path);
        //            product.photo3 = photo3.FileName;
        //        }
        //        if (photo4 != null && photo4.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo4.FileName));
        //            photo4.SaveAs(path);
        //            product.photo4 = photo4.FileName;
        //        }
        //        Pdao.Add(product);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(product);
        //    }
        //}

        //public ActionResult Edit(int id)
        //{
        //    List<Category> category = Cdao.ListCategory();
        //    SelectList categoryList = new SelectList(category, "id_category", "name", "id_category");
        //    ViewBag.CategoryList = categoryList;

        //    return View(Pdao.getByID(id));
        //}

        //[HttpPost]
        //public ActionResult Edit(int id, string name, string description, string information, string review, string availability, string price, string salePercent, string salePrice, string rate, HttpPostedFileBase mainPhoto, HttpPostedFileBase photo1, HttpPostedFileBase photo2, HttpPostedFileBase photo3, HttpPostedFileBase photo4, string updated, string id_category)
        //{
        //    Product product = Pdao.getByID(id);
        //    product.name = name;
        //    product.description = description;
        //    product.information = information;
        //    product.review = review;
        //    product.availability = Convert.ToBoolean(availability);
        //    product.price = Convert.ToDecimal(price);
        //    product.salePercent = Convert.ToInt32(salePercent);
        //    product.salePrice = Convert.ToDecimal(salePrice);
        //    product.rate = float.Parse(rate);
        //    product.updated = DateTime.Parse(updated);
        //    product.id_category = Convert.ToInt32(id_category);

        //    if (ModelState.IsValid)
        //    {
        //        if (mainPhoto != null && mainPhoto.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(mainPhoto.FileName));
        //            mainPhoto.SaveAs(path);
        //            product.mainPhoto = mainPhoto.FileName;
        //        }
        //        if (photo1 != null && photo1.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo1.FileName));
        //            photo1.SaveAs(path);
        //            product.photo1 = photo1.FileName;
        //        }
        //        if (photo2 != null && photo2.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo2.FileName));
        //            photo2.SaveAs(path);
        //            product.photo2 = photo2.FileName;
        //        }
        //        if (photo3 != null && photo3.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo3.FileName));
        //            photo3.SaveAs(path);
        //            product.photo3 = photo3.FileName;
        //        }
        //        if (photo4 != null && photo4.ContentLength > 0)
        //        {
        //            var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo4.FileName));
        //            photo4.SaveAs(path);
        //            product.photo4 = photo4.FileName;
        //        }
        //        Pdao.Edit(product);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(product);
        //    }
        //}

        //public ActionResult Delete(int id)
        //{
        //    Pdao.Delete(id);
        //    return RedirectToAction("Index");
        //}
    }
}
