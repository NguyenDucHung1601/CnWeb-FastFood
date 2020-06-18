using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoryDao dao = new CategoryDao();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(dao.ListCategory());
        }

        public ActionResult Details(int id)
        {
            Category category = dao.getByID(id);
            return View(category);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name)
        {
            Category category = new Category();
            category.name = name;

            if (ModelState.IsValid)
            {
                dao.Add(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult Edit(int id)
        {
            return View(dao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, string name)
        {
            Category category = dao.getByID(id);
            category.name = name;

            if (ModelState.IsValid)
            {
                dao.Edit(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult Delete(int id)
        {
            dao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}