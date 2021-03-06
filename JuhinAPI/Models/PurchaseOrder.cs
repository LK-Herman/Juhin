using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Models
{
    public class PurchaseOrder
    {
        [Key]
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        // Made unique manually via Fluent API
        [StringLength(50)]
        public string OrderNumber { get; set; }
        
        //PurchaseOrder m-1 Vendor
        [Required]
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }
        
        //PurchaseOrder m -1 User
        [Required]
        public string UserId { get; set; }
        //public IdentityUser User { get; set; }

        //PurchaseOrder m-m Deliveries
        public List<PurchaseOrder_Delivery> PurchaseOrderDeliveries { get; set; }

    }
}
