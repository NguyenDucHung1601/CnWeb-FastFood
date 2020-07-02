using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CnWeb_FastFood.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        SnackShopDBContext db = new SnackShopDBContext();
        CustomerDao Cdao = new CustomerDao();

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
            ViewBag.Count = Cdao.ListSimple(searching).Count();
            return View(Cdao.ListSimpleSearch(pageNumber, pagesize, searching));
        }

        public ActionResult Index2(int? page, int? PageSize, string idCustomer, string name, string phone, string address, string username)
        {
            ViewBag.IdCustomer = idCustomer;
            ViewBag.CustomerName = name;
            ViewBag.CustomerPhone = phone;
            ViewBag.CustomerAddress = address;
            ViewBag.CustomerUserName = username;

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
            ViewBag.Count = Cdao.ListAdvanced(idCustomer, name, phone, address, username).Count();
            return View(Cdao.ListAdvancedSearch(pageNumber, pagesize, idCustomer, name, phone, address, username));
        }

        public ActionResult Filter()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            Customer customer = Cdao.getByID(id);
            return View(customer);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string name, string phone, string address, string userName, string password, string subtotalCart, string totalCart, string id_discountCode)
        {
            Customer customer = new Customer();
            customer.name = name;
            customer.phone = phone;
            customer.address = address;
            customer.userName = userName;
            customer.password = password;
            customer.subtotalCart = Convert.ToDecimal(subtotalCart);
            customer.totalCart = Convert.ToInt32(totalCart);
            customer.id_discountCode = id_discountCode;

            if (ModelState.IsValid)
            {
                Cdao.Add(customer);
                return RedirectToAction("Index");
            }
            else
            {
                return View(customer);
            }
        }

        public ActionResult Edit(int id)
        {
            return View(Cdao.getByID(id));
        }

        [HttpPost]
        public ActionResult Edit(int id, string name, string phone, string address, string userName, string password, string subtotalCart, string totalCart, string id_discountCode)
        {
            Customer customer = Cdao.getByID(id);
            customer.name = name;
            customer.phone = phone;
            customer.address = address;
            customer.userName = userName;
            customer.password = password;
            customer.subtotalCart = Convert.ToDecimal(subtotalCart);
            customer.totalCart = Convert.ToInt32(totalCart);
            customer.id_discountCode = id_discountCode;

            if (ModelState.IsValid)
            {
                Cdao.Edit(customer);
                return RedirectToAction("Index");
            }
            else
            {
                return View(customer);
            }
        }

        public ActionResult Delete(int id)
        {
            Cdao.Delete(id);
            return RedirectToAction("Index");
        }
    }
}