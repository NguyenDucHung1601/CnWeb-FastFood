﻿using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class DiscountCodeController : Controller
    {
        SnackShopDBContext db = new SnackShopDBContext();
        DiscountCodeDao dao = new DiscountCodeDao();

        // GET: Admin/Customer
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
            ViewBag.Count = dao.ListSimple(searching).Count();
            return View(dao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Details(string id)
        {
            DiscountCode discount = dao.getByID(id);
            return View(discount);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string id, string discount)
        {
            DiscountCode discountCode = new DiscountCode();
            discountCode.id_discountCode = id;
            discountCode.discount = Convert.ToDecimal(discount);

            if (ModelState.IsValid)
            {
                dao.Add(discountCode);
                return RedirectToAction("Index");
            }
            else
            {
                return View(discountCode);
            }
        }

        public ActionResult Edit(string id)
        {
            return View(dao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, string discount)
        {
            DiscountCode discountCode = dao.getByID(id);
            discountCode.discount = Convert.ToDecimal(discount);

            if (ModelState.IsValid)
            {
                dao.Edit(discountCode);
                return RedirectToAction("Index");
            }
            else
            {
                return View(discountCode);
            }
        }

        public ActionResult Delete(string id)
        {
            dao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}