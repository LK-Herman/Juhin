﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class DeliveryCreationDTO
    {
        [Required]
        public DateTime CreatedAt { get; set; }
        
        public DateTime ETADate { get; set; }
        
        public DateTime DeliveryDate { get; set; }

        public int Rating { get; set; }
        public bool IsPriority { get; set; }
        public string Comment { get; set; }

        //Delivery m-1 Forwarder
        public int ForwarderId { get; set; }
        
        //Delivery m-1 Status
        public int StatusId { get; set; }
        
        //Delivery m-m PurchaseOrder
        //public List<PurchaseOrder_Delivery> PurchaseOrderDeliveries { get; set; }
    }
}
