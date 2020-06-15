namespace CnWeb_FastFood.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bill")]
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        [Key]
        public int id_bill { get; set; }

        public decimal? subtotal { get; set; }

        public decimal? total { get; set; }

        public DateTime? creatDate { get; set; }

        public int? id_customer { get; set; }

        [StringLength(20)]
        public string discountCode { get; set; }

        public decimal? discount { get; set; }

        public byte status { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
