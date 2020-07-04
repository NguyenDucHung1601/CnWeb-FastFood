using CnWeb_FastFood.Models.Dao.Client;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Controllers
{
    public class ShopDetailController : Controller
    {
        // GET: ShopDetail
        private SnackShopDBContext db = new SnackShopDBContext();
        private ShopDetailDao SDdao = new ShopDetailDao();
        public ActionResult Index(int? id)
        {
            Product product = db.Products.Find(id);
            var countReviews = product.review;
            product.review = countReviews + 1;
            db.SaveChanges();


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ViewBag.product = SDdao.GetProduct(id);
            var productDetailList = SDdao.GetProductDetail(id);
            if (productDetailList == null)
            {
                return HttpNotFound();
            }            

            return View(productDetailList);
           
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