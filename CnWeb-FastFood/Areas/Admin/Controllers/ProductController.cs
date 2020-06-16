using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductDao dao = new ProductDao();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(dao.ListProduct());
        }

        public ActionResult Details(int id)
        {
            Product product = dao.getByID(id);
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name)
        {
            Product product = new Product();
            product.name = name;

            if (ModelState.IsValid)
            {
                dao.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Edit(int id)
        {
            return View(dao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, string name, string description, string information, string review, bool availability, decimal price, int salePercent, decimal salePrice, float rate, string mainPhoto, string photo1, string photo2, string photo3, string photo4, DateTime updated, int id_category)
        {
            Product product = dao.getByID(id);
            product.name = name;
            product.description = description;
            product.information = information;
            product.review = review;
            product.availability = availability;
            product.price = price;
            product.salePercent = salePercent;
            product.salePrice = salePrice;
            product.rate = rate;
            product.mainPhoto = mainPhoto;
            product.photo1 = photo1;
            product.photo2 = photo2;
            product.photo3 = photo3;
            product.photo4 = photo4;
            product.updated = updated;
            product.id_category = id_category;

            if (ModelState.IsValid)
            {
                dao.Edit(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Delete(int id)
        {
            dao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}