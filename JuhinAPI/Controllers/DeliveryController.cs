﻿using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Helpers;
using JuhinAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.Extensions.Logging;
using JuhinAPI.Services;
using JuhinAPI.Data;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JuhinAPI.Controllers
{
    [ApiController]
    [Route("api/deliveries")]
    public class DeliveryController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<DeliveryController> logger;
        private readonly IEmailService emailService;
        private readonly IBackgroundJobClient backgroundJobs;

        public DeliveryController(
            ApplicationDbContext context, 
            IMapper mapper, 
            ILogger<DeliveryController> logger, 
            IEmailService emailService, 
            IBackgroundJobClient backgroundJobs)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
            this.emailService = emailService;
            this.backgroundJobs = backgroundJobs;
        }
        /// <summary>
        /// Gets all deliveries records from database
        /// </summary>
        /// <param name="pagination">(Page - page number to show / RecordsPerPage - How many records to show in one page.)</param>
        /// <returns></returns>
        [HttpGet(Name = "getDeliveries")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Deliveries
                .Include(d => d.Forwarder)
                .Include(d => d.PackedItems)
                .ThenInclude(x => x.Item)
                .ThenInclude(y => y.Unit)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .AsQueryable();
            
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var count = queryable.Count();
            HttpContext.Response.Headers.Add("All-Records", count.ToString());
            var deliveries = await queryable.Paginate(pagination).ToListAsync();
            
            return mapper.Map<List<DeliveryDetailsDTO>>(deliveries);
        }
        /// <summary>
        /// Gets all deliveries records from database
        /// </summary>
        /// <param name="orderId">Purchase order Id</param>
        /// <returns></returns>
        [HttpGet("byorder/{orderId}", Name = "GetByOrderId")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<PurchaseOrder_DeliveryDTO>>> GetByOrderId(Guid orderId)
        {
            var deliveries = await context.PurchaseOrders_Deliveries
                .Include(d =>d.Delivery)
                .Where(x => x.PurchaseOrderId == orderId)
                .ToListAsync();

            return mapper.Map<List<PurchaseOrder_DeliveryDTO>>(deliveries);
        }
        /// <summary>
        /// Shows only upcoming deliveries for next week (7 days) from today 
        /// </summary>
        /// <returns></returns>
        [HttpGet("upcoming/{startDate:DateTime}", Name ="GetUpcomingDeliveries")]
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> GetUpcomingDeliveries(DateTime startDate)
        {
            var weekAhead = startDate.AddDays(7);
            
            var upcomingDeliveries = await context.Deliveries
                .Include(d => d.Forwarder)
                .Include(d => d.PackedItems)
                .ThenInclude(i => i.Item)
                .ThenInclude(u => u.Unit)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .Where(d => d.ETADate <= weekAhead && d.ETADate >= startDate)
                .OrderBy(d => d.ETADate)
                .ToListAsync();

            return mapper.Map<List<DeliveryDetailsDTO>>(upcomingDeliveries);
        }
        /// <summary>
        /// Shows deliveries between two dates 
        /// </summary>
        /// <returns></returns>
        [HttpGet("dates/", Name = "GetDeliveriesByDates")]

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> GetDeliveriesByDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var deliveries = await context.Deliveries
                .Include(d => d.Forwarder)
                .Include(d => d.PackedItems)
                .ThenInclude(i => i.Item)
                .ThenInclude(u => u.Unit)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .Where(d => d.ETADate <= endDate && d.ETADate >= startDate)
                .OrderBy(d => d.ETADate)
                .ToListAsync();
            if (deliveries.Count == 0)
            {
                return NotFound("No deliveries found");
            }
            return mapper.Map<List<DeliveryDetailsDTO>>(deliveries);
        }
        /// <summary>
        /// Shows the deliveries after filtering by ETAdate, 
        /// OrderNumber or PartNumber. Displayed in ascending order by ETADate.
        /// </summary>
        /// <param name="filterDeliveriesDTO"></param>
        /// <returns></returns>
        [HttpGet("filter")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> GetFiltered([FromBody] FilterDeliveriesDTO filterDeliveriesDTO)
        {
            var nullDate = new DateTime();
            var nullGuid = new Guid();
            var deliveriesQueryable = context.Deliveries
                .Include(x => x.PackedItems)
                .ThenInclude(i => i.Item)
                .ThenInclude(u=>u.Unit)
                .Include(x => x.PurchaseOrderDeliveries)
                .ThenInclude(pod => pod.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .AsQueryable();
            
            
            if (filterDeliveriesDTO.StatusId != 0)
            {
                deliveriesQueryable = deliveriesQueryable.Where(s => s.StatusId == filterDeliveriesDTO.StatusId);
            }
            
            if (filterDeliveriesDTO.Date != nullDate)
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(d => d.ETADate > filterDeliveriesDTO.Date.AddDays(-10))
                    .Where(d => d.ETADate < filterDeliveriesDTO.Date.AddDays(10));
            }

            if (filterDeliveriesDTO.OrderId != nullGuid)
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PurchaseOrderDeliveries.Select(y => y.PurchaseOrderId)
                    .Contains(filterDeliveriesDTO.OrderId));
            }
            if (!string.IsNullOrWhiteSpace(filterDeliveriesDTO.VendorName))
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PurchaseOrderDeliveries
                        .Select(y => y.PurchaseOrder.Vendor)
                            .Select(v => v.Name)
                            .Contains(filterDeliveriesDTO.VendorName));
                    
                    
                    
                    //.Where(x => x.PurchaseOrderDeliveries.Any(p => p.PurchaseOrder.Vendor.Name.Contains(filterDeliveriesDTO.VendorName)));

            }

            if (!string.IsNullOrWhiteSpace(filterDeliveriesDTO.OrderNumber))
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PurchaseOrderDeliveries.Select(y => y.PurchaseOrder)
                    .Select(z => z.OrderNumber)
                    .Contains(filterDeliveriesDTO.OrderNumber));
            }

            if (!string.IsNullOrWhiteSpace(filterDeliveriesDTO.PartNumber))
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PackedItems.Select(y => y.Item)
                    .Select(z => z.Name)
                    .Contains(filterDeliveriesDTO.PartNumber));
            }

            if (!string.IsNullOrWhiteSpace(filterDeliveriesDTO.OrderingField))
            {
                //switch (filterDeliveriesDTO.OrderingField.ToUpper())
                //{
                //    case "ETADATE":
                //        break;
                //    case "DELIVERYDATE":
                //        break;
                //    case "RATING":
                //        break;
                //    default:
                //        filterDeliveriesDTO.OrderingField = "";
                //        break;
                //}
                try
                {
                    deliveriesQueryable = deliveriesQueryable
                        .OrderBy($"{filterDeliveriesDTO.OrderingField} {(filterDeliveriesDTO.AscendingOrder ? "ascending" : "descending")}" );
                }
                catch
                {
                    logger.LogWarning("Could not order by field " + filterDeliveriesDTO.OrderingField);
                }
            }
            var count = deliveriesQueryable.Count();
            HttpContext.Response.Headers.Add("All-Records", count.ToString());
            await HttpContext.InsertPaginationParametersInResponse(deliveriesQueryable, filterDeliveriesDTO.RecordsPerPage);
            var deliveries = await deliveriesQueryable.Paginate(filterDeliveriesDTO.Pagination).ToListAsync();

            return mapper.Map<List<DeliveryDetailsDTO>>(deliveries);
        }



        /// <summary>
        /// Finds deliveries by given data: 
        /// order number, part number or description, vendor name, ETA date, status (by id). Displayed in ascending order by ETADate.
        /// </summary>
        /// <param name="searchDeliveriesDTO"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> GetSearchedDeliveries([FromBody] SearchDeliveriesDTO searchDeliveriesDTO)
        {
            var nullDate = new DateTime();
            var deliveriesQueryable = context.Deliveries
                .Include(pi => pi.PackedItems).ThenInclude(i => i.Item).ThenInclude(u => u.Unit)
                .Include(pi => pi.PackedItems).ThenInclude(i => i.Item).ThenInclude(w => w.Warehouse)
                .Include(pod => pod.PurchaseOrderDeliveries).ThenInclude(po => po.PurchaseOrder).ThenInclude(v => v.Vendor)
                .AsQueryable();


            if (searchDeliveriesDTO.StatusId != 0)
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(s => s.StatusId == searchDeliveriesDTO.StatusId);
            }

            if (searchDeliveriesDTO.WarehouseId != 0)
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(s => s.PackedItems.Any(po => po.Item.Warehouse.WarehouseId == searchDeliveriesDTO.WarehouseId));
            }

            if (searchDeliveriesDTO.Date != nullDate)
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(d => d.ETADate > searchDeliveriesDTO.Date.AddDays(-3))
                    .Where(d => d.ETADate < searchDeliveriesDTO.Date.AddDays(3));
            }
                                  
            if (!string.IsNullOrWhiteSpace(searchDeliveriesDTO.VendorName))
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PurchaseOrderDeliveries.Any(p => p.PurchaseOrder.Vendor.Name.Contains(searchDeliveriesDTO.VendorName))
                    || x.PurchaseOrderDeliveries.Any(p => p.PurchaseOrder.Vendor.ShortName.Contains(searchDeliveriesDTO.VendorName)));
            }

            if (!string.IsNullOrWhiteSpace(searchDeliveriesDTO.OrderNumber))
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PurchaseOrderDeliveries.Any(p => p.PurchaseOrder.OrderNumber.Contains(searchDeliveriesDTO.OrderNumber)));
            }

            if (!string.IsNullOrWhiteSpace(searchDeliveriesDTO.PartDescription))
            {
                deliveriesQueryable = deliveriesQueryable
                    .Where(x => x.PackedItems.Any(i => i.Item.Name.Contains(searchDeliveriesDTO.PartDescription))
                    || x.PackedItems.Any(i => i.Item.Description.Contains(searchDeliveriesDTO.PartDescription)));
            }

            if (!string.IsNullOrWhiteSpace(searchDeliveriesDTO.OrderingField))
            {
                try
                {
                    deliveriesQueryable = deliveriesQueryable
                        .OrderBy($"{searchDeliveriesDTO.OrderingField} {(searchDeliveriesDTO.AscendingOrder ? "ascending" : "descending")}");
                }
                catch
                {
                    logger.LogWarning("Could not order by field " + searchDeliveriesDTO.OrderingField);
                }
            }
            var count = deliveriesQueryable.Count();
            HttpContext.Response.Headers.Add("All-Records", count.ToString());
            await HttpContext.InsertPaginationParametersInResponse(deliveriesQueryable, searchDeliveriesDTO.RecordsPerPage);
            var deliveries = await deliveriesQueryable.Paginate(searchDeliveriesDTO.Pagination).ToListAsync();

            return mapper.Map<List<DeliveryDetailsDTO>>(deliveries);
        }

        /// <summary>
        /// Gets the delivery data requested by Id
        /// </summary>
        /// <param name="id">Requested Id of delivery record</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetDelivery")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<DeliveryDTO>> GetDeliveryById(Guid id)
        {
            var delivery = await context.Deliveries
                .Where(d => d.DeliveryId == id)
                .Include(d => d.Forwarder)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .FirstOrDefaultAsync();
            if (delivery == null)
            {
                return NotFound();
            }
            return mapper.Map<DeliveryDTO>(delivery);
        }
        /// <summary>
        /// Gets detailed delivery data requested by Id
        /// </summary>
        /// <param name="deliveryId">Requested delivery Id </param>
        /// <returns></returns>
        [HttpGet("detailed/{deliveryId:Guid}", Name = "GetDetailed")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman,Guest")]
        public async Task<ActionResult<DeliveryDetailsDTO>> GetDeliveryByIdDetailed(Guid deliveryId)
        {
            var delivery = await context.Deliveries
                .Where(d => d.DeliveryId == deliveryId)
                .Include(d => d.Forwarder)
                .Include(d => d.PackedItems)
                .ThenInclude(i => i.Item)
                .ThenInclude(u =>u.Unit)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .FirstOrDefaultAsync();
            if (delivery == null)
            {
                return NotFound();
            }
            return mapper.Map<DeliveryDetailsDTO>(delivery);
        }
        /// <summary>
        /// Creates new delivery 
        /// </summary>
        /// <param name="purchaseOrderId">The Id of the Purchase Order related to the delivery</param>
        /// <param name="newDelivery">New delivery data</param>
        /// <returns></returns>
        [HttpPost("{purchaseOrderId:Guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Post(Guid purchaseOrderId, [FromBody] DeliveryCreationDTO newDelivery)
        {
            var delivery = mapper.Map<Delivery>(newDelivery);
            delivery.StatusId = 1;
            context.Add(delivery);
            var purchaseOrderDelivery = new PurchaseOrder_Delivery() {PurchaseOrderId = purchaseOrderId, DeliveryId = delivery.DeliveryId };
            context.Add(purchaseOrderDelivery);
            
            await context.SaveChangesAsync();
            
            var deliveryDTO = mapper.Map<DeliveryDTO>(delivery);
            return new CreatedAtRouteResult("GetDelivery", deliveryDTO);
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendStatusChangeEmail(Guid deliveryId, int previousStatusId) 
        {
            var delivery = context.Deliveries
               .Where(d => d.DeliveryId == deliveryId)
               .Include(s => s.PackedItems)
                    .ThenInclude(i => i.Item)
                    .ThenInclude(u => u.Unit)
               .Include(s => s.Forwarder)
               .Include(s => s.Status)
               .Include(s => s.PurchaseOrderDeliveries)
                   .ThenInclude(y => y.PurchaseOrder)
                   .ThenInclude(y => y.Vendor)
               .AsNoTracking()
               .FirstOrDefault();

            if (delivery.StatusId != previousStatusId)
            {
                var actualStatus =  context.Statuses
                    .Where(s => s.StatusId == delivery.StatusId)
                    .AsNoTracking()
                    .FirstOrDefault();

                var subscribersIds =  context.Subscriptions
                    .Where(s => s.DeliveryId == delivery.DeliveryId)
                    .Select(x => x.UserId)
                    .AsNoTracking()
                    .ToList();

                string statusColorDark = "#ffae4c";
                string statusColorBright = "#ffde7c";
                if(actualStatus.Name.ToUpper() == "DELIVERED")
                {
                    statusColorBright = "#91ff37";
                    statusColorDark = "#63ac00";
                }
                string packingList = "<p style=\"color:#ff9d9d\">Packing list is empty (no parts added)</p>";
                int i = 0;
                if (delivery.PackedItems.Count != 0) 
                {
                    packingList = "";
                    foreach (var packedItem in delivery.PackedItems)
                    {
                        i++;
                        packingList = packingList + "<p style=\"color:#e3e3e3\">" + i+")   " +packedItem.Item.Name.ToString() + " - " + packedItem.Quantity.ToString() + " " + packedItem.Item.Unit.ShortName + "</p>";
                    }
                }

                var message = new EmailMessage();
                message.Subject = "JuhinAPI Status Notification";
                message.Content =
                    "<div style=\"margin: 10px; width:400px; font-family: Calibri; font-size:16px; border-radius:16px; overflow:hidden; background-image: linear-gradient(#161f24, #727272, #969696); background-color: #95bebe;\">" +
                        "<div style=\"border-radius:0px; text-align:center; padding:2px; margin:0px; color:white; font-size:20px; background-image: linear-gradient(#00cfeb,#008fa8,#006f88, #006f88, #005f78);\">" +
                            "<p style=\"margin-bottom: 2px;\"> DELIVERY SUBSCRIPTION NOTICE</p>" +
                            "<p style=\"font-size:16px; margin-top: 2px;\">Status of your delivery has been updated.</p>" +
                        "</div>" +
                        "<div style=\"padding:5px 10px 5px 40px; color: #e3e3e3\">" +
                            "<p style=\"color:#e3e3e3\"><b>Supplier</b>: " + delivery.PurchaseOrderDeliveries[0].PurchaseOrder.Vendor.Name + "</p>" +
                            "<p style=\"color:#e3e3e3\"><b>PO Number</b>: " + delivery.PurchaseOrderDeliveries[0].PurchaseOrder.OrderNumber.ToString() + " </p>" +
                            "<p style=\"color:#e3e3e3\"><b>Forwarder</b>: " + delivery.Forwarder.Name.ToString() + " </p>" +
                            "<p style=\"color:#e3e3e3\"><b>Delivery date</b>: " + delivery.DeliveryDate.ToLocalTime().ToString() + " </p>" +
                            "<p style=\"color:#e3e3e3\"><b>ETA</b>: " + delivery.ETADate.ToLocalTime().ToString() + " </p>" +
                            "<p style=\"color:#e3e3e3\"><b>Packing list</b>: </p>" + packingList +  
                            "<p style=\"color:#e3e3e3\"><b>Comment</b>: <span style=\"color:#73e3c3\">" + delivery.Comment.ToString() + "</span> </p>" +
                        "</div>" +
                        "<p style=\"width: 380px; font-size:20px; color:#262626; text-align:center; padding:10px; margin:0px; transform: matrix(1,-0.3,0.3,1,100, -18); background-image: linear-gradient("+ statusColorBright+ ", "+ statusColorDark +");\"><b>" + actualStatus.Name.ToUpper() + "</b></p>" +
                    "</div>";
                message.FromAddress.Name = "JuhinAPI Software";
                message.FromAddress.Address = "pipsitestemail@gmail.com";
                
                foreach (var subId in subscribersIds)
                {
                    var email = context.Users
                        .Where(x => x.Id == subId)
                        .AsNoTracking()
                        .FirstOrDefault();
                       
                    message.ToAddresses.Add(new EmailAddress { Name = "Dear "+email.UserName.ToLower(), Address = email.Email });
                }
                try
                {
                    backgroundJobs.Enqueue(() => emailService.Send(message));
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                    var errorMessage = new EmailMessage();
                    errorMessage.Content = "<p>ERROR MESSAGE: " + ex.Message + " / " + ex.TargetSite.Name.ToString() + "</p>";
                    errorMessage.Subject = "JuhinAPI ERROR/EXCEPTION Notification";
                    errorMessage.FromAddress.Name = "JuhinAPI Software";
                    errorMessage.ToAddresses.Add(new EmailAddress { Name = "Hermano", Address = "lkuczma@gmail.com" });
                    errorMessage.FromAddress.Address = "pipsitestemail@gmail.com";
                    emailService.Send(errorMessage);
                }
               // emailService.Send(message);
            }

        }

        /// <summary>
        /// Edits the delivery data requested by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedDelivery"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist,Warehouseman")]
        public async Task<ActionResult> Put(Guid id, [FromBody] DeliveryCreationDTO updatedDelivery)
        {
            var oldStatusId = await context.Deliveries
                 .Where(d => d.DeliveryId == id)
                 .Select(si => si.StatusId)
                 .FirstOrDefaultAsync();

            var delivery = mapper.Map<Delivery>(updatedDelivery);
            delivery.DeliveryId = id;
            if(delivery.StatusId == 3)
            {
                delivery.DeliveryDate = DateTime.Now;
            }
            context.Entry(delivery).State = EntityState.Modified;
            await context.SaveChangesAsync();

            SendStatusChangeEmail(id, oldStatusId);

            return NoContent();
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="deliveryId"></param>
       /// <param name="purchaseOrderId"></param>
       /// <returns></returns>
        [HttpDelete("{deliveryId},{purchaseOrderId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Specialist")]
        public async Task<ActionResult> Delete(Guid deliveryId, Guid purchaseOrderId )
        {
            var deliveryExist = await context.Deliveries.AnyAsync(d => d.DeliveryId == deliveryId);
            if (!deliveryExist)
            {
                return NotFound();
            }
            var purchaseOrderDelivery = await context.PurchaseOrders_Deliveries
                .Where(pod => pod.DeliveryId == deliveryId)
                .Where(pod => pod.PurchaseOrderId == purchaseOrderId)
                .FirstOrDefaultAsync();
            if (purchaseOrderDelivery == null)
            {
                return NotFound();
            }
            
            context.Remove(purchaseOrderDelivery);
            await context.SaveChangesAsync();
            context.Remove(new Delivery() { DeliveryId = deliveryId });
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
