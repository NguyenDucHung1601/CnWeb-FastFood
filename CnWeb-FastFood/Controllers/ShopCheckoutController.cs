using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CnWeb_FastFood.Controllers
{
    public class ShopCheckoutController : Controller
    {
        // GET: ShopCheckout
        public static Checkout bill;
        public ActionResult Index()
        {
            if (Session["ID_SESSION"] != null)
            {
                ViewBag.customer = new CustomerDao().getByID(Convert.ToInt32(Session["ID_SESSION"]));
            }
                
            return View(bill);
        }

        public ActionResult AddBill(string billModel)
        {
            var jsonBill = new JavaScriptSerializer().Deserialize<Checkout>(billModel);
            List<CartItem> listBill = jsonBill.Item;
            ProductDao pdao = new ProductDao();
            foreach (var line in listBill)
            {
                line.Products = pdao.getByID(line.Products.id_product);
                line.Price = line.Products.price.GetValueOrDefault(0);
                line.IntoMoney = line.Price * line.Amount;
            }
            jsonBill.Item = listBill;
            bill = jsonBill;

            return Json(new { newUrl = Url.Action("Index", "ShopCheckout") });
        }
    }
}