namespace CnWeb_FastFood.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            BillDetails = new HashSet<BillDetail>();
            CartDetails = new HashSet<CartDetail>();
            ProductDetails = new HashSet<ProductDetail>();
        }

        [Key]
        public int id_product { get; set; }

        [StringLength(200)]
        [Display(Name ="Tên sản phẩm")]
        public string name { get; set; }

        [Display(Name = "Mô tả")]
        public string description { get; set; }

        [Display(Name = "Thông tin")]
        public string information { get; set; }

        [Display(Name = "Đánh giá")]
        public string review { get; set; }

        [Display(Name = "Sẵn có")]
        public bool availability { get; set; }

        [Display(Name = "Giá")]
        public decimal? price { get; set; }

        [Display(Name = "% Giảm giá")]
        public int? salePercent { get; set; }

        [Display(Name = "Giá sale")]
        public decimal? salePrice { get; set; }

        [Display(Name = "Mức đánh giá")]
        public double? rate { get; set; }

        [StringLength(100)]
        [Display(Name = "Hình ảnh chính")]
        public string mainPhoto { get; set; }

        [StringLength(100)]
        [Display(Name = "Hình ảnh phụ 1")]
        public string photo1 { get; set; }

        [StringLength(100)]
        [Display(Name = "Hình ảnh phụ 2")]
        public string photo2 { get; set; }

        [StringLength(100)]
        [Display(Name = "Hình ảnh phụ 3")]
        public string photo3 { get; set; }

        [StringLength(100)]
        [Display(Name = "Hình ảnh phụ 4")]
        public string photo4 { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày cập nhật")]
        public DateTime? updated { get; set; }

        public int? id_category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BillDetail> BillDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
