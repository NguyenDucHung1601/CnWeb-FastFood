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


namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        SnackShopDBContext db = new SnackShopDBContext();
        ProductDao Pdao = new ProductDao();
        CategoryDao Cdao = new CategoryDao();

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
            ViewBag.Count = Pdao.ListSimple(searching).Count();
            return View(Pdao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Index2(int? page, int? PageSize, string idProduct, string name, string category, string availability, string priceFrom, string priceTo, string salePercentFrom, string salePercentTo, string salePriceFrom, string salePriceTo, string rateFrom, string rateTo)
        {
            ViewBag.IdProduct = idProduct;
            ViewBag.ProductName = name;
            ViewBag.IdCategory = category;
            if (category != "" && category != null)
                ViewBag.CategoryName = Cdao.getByID(Convert.ToInt32(category)).name;
            ViewBag.Availability = availability;
            ViewBag.PriceFrom = priceFrom;
            ViewBag.PriceTo = priceTo;
            ViewBag.SalePercentFrom = salePercentFrom;
            ViewBag.SalePercentTo = salePercentTo;
            ViewBag.SalePriceForm = salePriceFrom;
            ViewBag.SalePriceTo = salePriceTo;
            ViewBag.RateFrom = rateFrom;
            ViewBag.RateTo = rateTo;

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
            ViewBag.Count = Pdao.ListAdvanced(idProduct, name, category, availability, priceFrom, priceTo, salePercentFrom, salePercentTo, salePriceFrom, salePriceTo, rateFrom, rateTo).Count();
            return View(Pdao.ListAdvancedSearch(pageNumber, pagesize, idProduct, name, category, availability, priceFrom, priceTo, salePercentFrom, salePercentTo, salePriceFrom, salePriceTo, rateFrom, rateTo));
        }

        public ActionResult Filter()
        {
            List<Category> ctgr = Cdao.ListCategory();
            SelectList categoryList = new SelectList(ctgr, "id_category", "name", "id_category");
            ViewBag.CategoryList = categoryList;

            return View();
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
            product.review = Convert.ToInt32(review);
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
            product.review = Convert.ToInt32(review);
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