using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CnWeb_FastFood.Models.EF;

namespace CnWeb_FastFood.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private SnackShopDBContext db = new SnackShopDBContext();
        public ActionResult Index(string searchString, string catelogyString)
        {
            ViewBag.searchString = searchString;
            ViewBag.catelogyString = catelogyString;
            IEnumerable< ProductView> list ;
            if(catelogyString == "All Categories")
            {
                catelogyString = "";
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                list = db.Database.SqlQuery<ProductView>($"SELECT p.id_product, p.name as productName, p.id_category, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.mainPhoto, p.updated " +
                $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category where c.[name] LIKE N'%{catelogyString}%'").ToList();
            }
            list = db.Database.SqlQuery<ProductView>($"SELECT p.id_product, p.name as productName, p.id_category, c.name as categoryName, p.availability, p.price, p.salePercent, p.salePrice, p.rate, p.mainPhoto, p.updated " +
                $"FROM dbo.Product p LEFT JOIN dbo.Category c ON c.id_category = p.id_category " +
                $"WHERE c.[name] LIKE N'%{catelogyString}%' AND p.id_product LIKE N'%{searchString}%' " +
                $"OR p.name LIKE N'%{searchString}%' " +
                $"OR c.name LIKE N'%{searchString}%' " +
                $"OR p.availability LIKE N'%{searchString}%' " +
                $"OR p.price LIKE N'%{searchString}%' " +
                $"OR p.salePercent LIKE N'%{searchString}%' " +
                $"OR p.salePrice LIKE N'%{searchString}%' " +
                $"OR p.rate LIKE N'%{searchString}%' " +
                $"OR p.updated LIKE N'%{searchString}%'").ToList();

            
            return View(list);
        }
        public ActionResult CategoryShow()
        {           
            return PartialView(db.Categories.ToList());
        }
        public ActionResult CategoryShowImage()
        {
            return PartialView(db.Categories.ToList());
        }
        public ActionResult ListCategoryShow()
        {
            return PartialView(db.Categories.ToList());
        }

        public ActionResult LatestProducts()
        {
            return PartialView(db.Products.OrderByDescending(p => p.updated).Take(5));
        }
        
        public ActionResult TopRatedProducts()
        {
            return PartialView(db.Products.OrderByDescending(p => p.rate).Take(5));
        }
        public ActionResult ReviewProducts()
        {
            return PartialView(db.Products.OrderByDescending(p => p.review).Take(5));
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