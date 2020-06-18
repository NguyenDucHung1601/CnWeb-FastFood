using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class CustomerDao
    {
        SnackShopDBContext db = null;
        public CustomerDao()
        {
            db = new SnackShopDBContext();
        }
        public int Delete(int? id)
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
            return db.Customers.OrderByDescending(c => c.id_customer).ToPagedList(PageNum, PageSize);
        }

        public void Add(Customer customer)
        {
            customer.subtotalCart = 0;
            customer.totalCart = 0;
            db.Customers.Add(customer);
            db.SaveChanges();
        }
    }
}