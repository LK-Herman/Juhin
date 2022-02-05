using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class SearchDeliveriesDTO
    {
        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 20;
        public PaginationDTO Pagination
        {
            get { return new PaginationDTO() { Page = Page, RecordsPerPage = RecordsPerPage }; }
        }

        public DateTime Date { get; set; }
        public int StatusId { get; set; }
        public int WarehouseId { get; set; }
        public string VendorName { get; set; }
        public string OrderNumber { get; set; }
        public string PartDescription { get; set; }
        public string OrderingField { get; set; }
        public string Country { get; set; }
        public bool isPrio { get; set; }
        public bool isICP { get; set; }
        public bool AscendingOrder { get; set; } = true;
    }
}
