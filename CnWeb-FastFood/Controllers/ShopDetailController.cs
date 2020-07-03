using CnWeb_FastFood.Models.Dao.Client;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = SDdao.getProduct(id);
            if (BillDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_bill = id;

            return PartialView(BillDetail);
            return View();
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