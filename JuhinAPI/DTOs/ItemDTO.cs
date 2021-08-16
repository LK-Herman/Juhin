﻿using JuhinAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class ItemDTO
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RevisionNumber { get; set; }
        public int Price { get; set; }
        public int MaxEuroPalQty { get; set; }
        public bool IsICP { get; set; }

        //Vendor 1-m Item 
        public VendorDTO Vendor { get; set; }

        //Currency 1-m Item
        public CurrencyDTO Currency { get; set; }

        //PalletType 1-m Item
        public PalletDTO Pallet { get; set; }

        //Item 1-1 PackedItem *********************************** change to DTO
        public PackedItem PackedItem { get; set; }

        //Item m-1 Unit
        public UnitDTO Unit { get; set; }
    }
}
