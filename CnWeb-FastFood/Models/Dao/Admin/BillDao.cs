using CnWeb_FastFood.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CnWeb_FastFood.Models.Dao.Admin
{
    public class BillDao
    {
        SnackShopDBContext db = null;
        public BillDao()
        {
            db = new SnackShopDBContext();

        }

        public IEnumerable<BillDetail> BillDetail(int? id)
        {            
            return db.BillDetails.ToList().Where(bd => bd.id_bill == id);
        }
    }
}
