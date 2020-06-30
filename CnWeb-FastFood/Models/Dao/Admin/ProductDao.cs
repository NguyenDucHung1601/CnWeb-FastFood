using CnWeb_FastFood.Models.EF;
using PagedList;
//using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class ProductDao
    {
        SnackShopDBContext db;

        public ProductDao()
        {
            db = new SnackShopDBContext();
        }

        public List<Product> ListProduct()
        {
            return db.Products.ToList();
        }

        public Product getByID(int id)
        {
            return db.Products.Find(id);
        }

        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Edit(Product product)
        {
            Product prd = getByID(product.id_product);
            if (prd != null)
            {
                prd.name = product.name;
                prd.description = product.description;
                prd.information = product.information;
                prd.review = product.review;
                prd.availability = product.availability;
                prd.price = product.price;
                prd.salePercent = product.salePercent;
                prd.salePrice = product.salePrice;
                prd.rate = product.rate;
                prd.mainPhoto = product.mainPhoto;
                prd.photo1 = product.photo1;
                prd.photo2 = product.photo2;
                prd.photo3 = product.photo3;
                prd.photo4 = product.photo4;
                prd.updated = product.updated;
                prd.id_category = product.id_category;

                db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            Product product = db.Products.Find(id);
            if (product != null)
            {
                db.Products.Remove(product);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<Product> ListProductPage(int PageNum, int PageSize)
        {
            return db.Products.OrderBy(p=>p.id_product).ToPagedList(PageNum, PageSize);
        }

        public IEnumerable<Product> ListProductSimpleSearch(string SearchString)
        {
            List<Product> list = db.Database.SqlQuery<Product>("SELECT name FROM dbo.Product").ToList();
            return list;
        }
    }
}