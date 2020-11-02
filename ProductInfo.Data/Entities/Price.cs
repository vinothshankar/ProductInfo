using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductInfo.Data.Entities
{
    public partial class Price
    {
        [Key]
        public long Id { get; set; }
        public long ProductId { get; set; }
        [Column("Price", TypeName = "decimal(18, 0)")]
        public decimal Price1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FromDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }

       [NotMapped]
        public virtual Product IdNavigation { get; set; }
    }
}
