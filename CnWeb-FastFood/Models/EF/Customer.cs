namespace CnWeb_FastFood.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Bills = new HashSet<Bill>();
            Carts = new HashSet<Cart>();
        }

        [Key]
        public int id_customer { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên khách hàng")]
        public string name { get; set; }

        [StringLength(10)]
        [Display(Name = "Điện thoại")]
        public string phone { get; set; }

        [StringLength(200)]
        [Display(Name = "Địa chỉ")]
        public string address { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        public string userName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }

        [Display(Name = "Tổng")]
        public decimal? subtotalCart { get; set; }

        [Display(Name = "Tổng tiền")]
        public decimal? totalCart { get; set; }

        [StringLength(20)]
        public string id_discountCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        public virtual DiscountCode DiscountCode { get; set; }
    }
}
