namespace CnWeb_FastFood.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Credential")]
    public partial class Credential
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string id_userGroup { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string id_role { get; set; }
    }
}
