using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductInfo.Data.Entities
{
    public partial class Product
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(128)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(512)]
        public string Description { get; set; }
        public int? Quantity { get; set; }

        [NotMapped]
        public virtual Price Price { get; set; }

        public bool IsActive { get; set; }
    }
}
