using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.EF
{
    [Serializable]
    public class Checkout
    {
        public List<CartItem> Item { set; get; }
        public int? Id_customer { get; set; }
        public decimal Subtotal { get; set; }

        public decimal Total { get; set; }
        public Checkout()
        {
            Subtotal = 0;
            Total = 0;
        }
    }
}