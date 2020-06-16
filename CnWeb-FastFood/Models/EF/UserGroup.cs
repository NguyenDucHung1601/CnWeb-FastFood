namespace CnWeb_FastFood.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserGroup")]
    public partial class UserGroup
    {
        [Key]
        [StringLength(20)]
        public string id_userGroup { get; set; }

        [StringLength(50)]
        public string name { get; set; }
    }
}
