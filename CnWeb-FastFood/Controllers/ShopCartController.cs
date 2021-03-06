﻿using CnWeb_FastFood.Models.Dao.Admin;
using CnWeb_FastFood.Models.EF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CnWeb_FastFood.Controllers
{
    public class ShopCartController : Controller
    {
        private const string CartSession = "CartSession";
        private const string SubTotalCartSession = "SubTotalCartSession";
        private const string TotalCartSession = "TotalCartSession";
        

        // GET: ShopCart
        public ActionResult Index(string discountCode)
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();           
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            decimal discount = 0;
            if (!string.IsNullOrEmpty(discountCode))
            {
                var dc = new DiscountCodeDao().getByID(discountCode);
                discount = dc.discount.GetValueOrDefault(0);
                
            }
            decimal subtotal = list.Sum(x => x.IntoMoney);
            decimal total = subtotal - discount;
            if (total < 0) total = 0;
            Session[SubTotalCartSession]=subtotal;
            Session[TotalCartSession] = total;
            ViewBag.discountCode = discountCode;

            return View(list);
        }


        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDao().getByID(productId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Products.id_product == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Products.id_product == productId)
                        {
                            item.Amount += quantity;
                            item.Price = item.Products.price.GetValueOrDefault(0);
                            item.IntoMoney = item.Price * item.Amount;
                        }
                    }
                }
                //trong giỏ hàng chưa có
                // tạo mới 1 đối tượng cart item
                else
                {
                    var item = new CartItem();
                    item.Products = product;
                    item.Amount = quantity;
                    item.Price = item.Products.price.GetValueOrDefault(0);
                    item.IntoMoney = item.Price * item.Amount;
                    list.Add(item);

                }
                Session[CartSession] = list;

            }
            else
            {
                var item = new CartItem();
                item.Products = product;
                item.Amount = quantity;
                item.Price = item.Products.price.GetValueOrDefault(0);
                item.IntoMoney = item.Price * item.Amount;
                var list = new List<CartItem>();
                list.Add(item);

                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult AddToDetail(string product)
        {

            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(product);
            var item = jsonCart.ElementAt(0);
            AddItem(item.Products.id_product, item.Amount);
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return Json(new
            {
                status = true,
                count = list.Count,
                total = list.Sum(x => x.IntoMoney)
            });
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Products.id_product == item.Products.id_product);
                if (jsonItem != null)
                {
                    item.Amount = jsonItem.Amount;
                    item.Price = item.Products.price.GetValueOrDefault(0);
                    item.IntoMoney = item.Price * item.Amount;
                }

            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult HeaderCart()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        public ActionResult Delete(int? id)
        {

            var sessionCart = (List<CartItem>)Session[CartSession];
            var item = sessionCart.SingleOrDefault(x => x.Products.id_product == id);
            sessionCart.Remove(item);
            return RedirectToAction("Index");
        }

     
        

    }
}