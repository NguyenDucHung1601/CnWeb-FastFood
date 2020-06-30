using CnWeb_FastFood.Models.EF;
using PagedList;
//using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class CustomerDao
    {
        SnackShopDBContext db;

        public CustomerDao()
        {
            db = new SnackShopDBContext();
        }

        public List<Customer> ListCustomer()
        {
            return db.Customers.ToList();
        }

        public Customer getByID(int id)
        {
            return db.Customers.Find(id);
        }

        public void Add(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public void Edit(Customer customer)
        {
            Customer ctm = getByID(customer.id_customer);
            if (ctm != null)
            {
                ctm.name = customer.name;
                ctm.phone = customer.phone;
                ctm.address = customer.address;
                ctm.userName = customer.userName;
                ctm.password = customer.password;
                ctm.subtotalCart = customer.subtotalCart;
                ctm.totalCart = customer.totalCart;
                ctm.id_discountCode = customer.id_discountCode;

                db.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer != null)
            {
                db.Customers.Remove(customer);
                return db.SaveChanges();
            }
            else
            {
                return -1;
            }
        }

        public IEnumerable<Customer> ListCustomerPage(int PageNum, int PageSize)
        {
            return db.Customers.OrderBy(p => p.id_customer).ToPagedList(PageNum, PageSize);
        }
    }
}