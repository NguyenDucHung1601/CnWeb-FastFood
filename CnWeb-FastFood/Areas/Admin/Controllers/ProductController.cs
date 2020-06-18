using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductDao Pdao = new ProductDao();
        CategoryDao Cdao = new CategoryDao();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(Pdao.ListProduct());
        }

        public ActionResult Details(int id)
        {
            Product product = Pdao.getByID(id);
            return View(product);
        }

        public ActionResult Create()
        {
            List<Category> category = Cdao.ListCategory();
            SelectList categoryList = new SelectList(category, "id_category", "name", "id_category");
            ViewBag.CategoryList = categoryList;

            return View();
        }

        [HttpPost]
        public ActionResult Create(string name, string description, string information, string review, string availability, string price, string salePercent, string salePrice, string rate, HttpPostedFileBase mainPhoto, HttpPostedFileBase photo1, HttpPostedFileBase photo2, HttpPostedFileBase photo3, HttpPostedFileBase photo4, string updated, string id_category)
        {
            Product product = new Product();
            product.name = name;
            product.description = description;
            product.information = information;
            product.review = review;
            product.availability = Convert.ToBoolean(availability);
            product.price = Convert.ToDecimal(price);
            product.salePercent = Convert.ToInt32(salePercent);
            product.salePrice = Convert.ToDecimal(salePrice);
            product.rate = float.Parse(rate);
            product.updated = DateTime.Parse(updated);
            product.id_category = Convert.ToInt32(id_category);

            if (ModelState.IsValid)
            {
                if (mainPhoto != null && mainPhoto.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(mainPhoto.FileName));
                    mainPhoto.SaveAs(path);
                    product.mainPhoto = mainPhoto.FileName;
                }
                if (photo1 != null && photo1.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo1.FileName));
                    photo1.SaveAs(path);
                    product.photo1 = photo1.FileName;
                }
                if (photo2 != null && photo2.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo2.FileName));
                    photo2.SaveAs(path);
                    product.photo2 = photo2.FileName;
                }
                if (photo3 != null && photo3.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo3.FileName));
                    photo3.SaveAs(path);
                    product.photo3 = photo3.FileName;
                }
                if (photo4 != null && photo4.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo4.FileName));
                    photo4.SaveAs(path);
                    product.photo4 = photo4.FileName;
                }
                Pdao.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Edit(int id)
        {
            List<Category> category = Cdao.ListCategory();
            SelectList categoryList = new SelectList(category, "id_category", "name", "id_category");
            ViewBag.CategoryList = categoryList;

            return View(Pdao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, string name, string description, string information, string review, string availability, string price, string salePercent, string salePrice, string rate, HttpPostedFileBase mainPhoto, HttpPostedFileBase photo1, HttpPostedFileBase photo2, HttpPostedFileBase photo3, HttpPostedFileBase photo4, string updated, string id_category)
        {
            Product product = Pdao.getByID(id);
            product.name = name;
            product.description = description;
            product.information = information;
            product.review = review;
            product.availability = Convert.ToBoolean(availability);
            product.price = Convert.ToDecimal(price);
            product.salePercent = Convert.ToInt32(salePercent);
            product.salePrice = Convert.ToDecimal(salePrice);
            product.rate = float.Parse(rate);
            product.updated = DateTime.Parse(updated);
            product.id_category = Convert.ToInt32(id_category);

            if (ModelState.IsValid)
            {
                if (mainPhoto != null && mainPhoto.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(mainPhoto.FileName));
                    mainPhoto.SaveAs(path);
                    product.mainPhoto = mainPhoto.FileName;
                }
                if (photo1 != null && photo1.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo1.FileName));
                    photo1.SaveAs(path);
                    product.photo1 = photo1.FileName;
                }
                if (photo2 != null && photo2.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo2.FileName));
                    photo2.SaveAs(path);
                    product.photo2 = photo2.FileName;
                }
                if (photo3 != null && photo3.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo3.FileName));
                    photo3.SaveAs(path);
                    product.photo3 = photo3.FileName;
                }
                if (photo4 != null && photo4.ContentLength > 0)
                {
                    var path = Path.Combine(Server.MapPath("~/Areas/Admin/Content/Photos/"), System.IO.Path.GetFileName(photo4.FileName));
                    photo4.SaveAs(path);
                    product.photo4 = photo4.FileName;
                }
                Pdao.Edit(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult Delete(int id)
        {
            Pdao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}