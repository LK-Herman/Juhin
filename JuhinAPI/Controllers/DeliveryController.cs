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

namespace JuhinAPI.Controllers
{
    [ApiController]
    [Route("api/deliveries")]
    public class DeliveryController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DeliveryController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> Get([FromQuery] PaginationDTO pagination)
        {
            var queryable = context.Deliveries
                .Include(d => d.Forwarder)
                .Include(d => d.PackedItems)
                .Include(d => d.Status)
                .Include(d => d.PurchaseOrderDeliveries)
                .ThenInclude(p => p.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .AsQueryable();
            
            await HttpContext.InsertPaginationParametersInResponse(queryable, pagination.RecordsPerPage);
            var deliveries = await queryable.Paginate(pagination).ToListAsync();
            
            return mapper.Map<List<DeliveryDetailsDTO>>(deliveries);
        }

        [HttpGet]
        [Route("upcoming")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> GetUpcomingDeliveries()
        {
            var weekAhead = DateTime.Today.AddDays(7);
            
            var upcomingDeliveries = await context.Deliveries
                .Include(x => x.PackedItems)
                .ThenInclude(i => i.Item)
                .Include(x => x.PurchaseOrderDeliveries)
                .ThenInclude(pod => pod.PurchaseOrder)
                .ThenInclude(p => p.Vendor)
                .Include(x => x.Forwarder)
                .Where(d => d.ETADate < weekAhead)
                .Where(s => s.StatusId == 1 )
                .OrderBy(d => d.ETADate)
                .ToListAsync();

            return mapper.Map<List<DeliveryDetailsDTO>>(upcomingDeliveries);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<DeliveryDetailsDTO>>> GetFiltered([FromQuery] FilterDeliveriesDTO filterDeliveriesDTO)
        {
            var nullDate = new DateTime();
            var nullGuid = new Guid();
            var deliveriesQueryable = context.Deliveries
                .Include(x => x.PackedItems)
                .ThenInclude(i => i.Item)
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

            await HttpContext.InsertPaginationParametersInResponse(deliveriesQueryable, filterDeliveriesDTO.RecordsPerPage);
            var deliveries = await deliveriesQueryable.Paginate(filterDeliveriesDTO.Pagination).ToListAsync();

            return mapper.Map<List<DeliveryDetailsDTO>>(deliveries);
        }



        [HttpGet("{id}", Name = "GetDelivery")]
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

        [HttpGet("{deliveryId:Guid}", Name = "GetDetailed")]
        public async Task<ActionResult<DeliveryDetailsDTO>> GetDeliveryByIdDetailed(Guid deliveryId)
        {
            var delivery = await context.Deliveries
                .Where(d => d.DeliveryId == deliveryId)
                .Include(d => d.Forwarder)
                .Include(d => d.PackedItems)
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

        [HttpPost("{purchaseOrderId:Guid}")]
        public async Task<ActionResult> Post(Guid purchaseOrderId, [FromBody] DeliveryCreationDTO newDelivery)
        {
            var delivery = mapper.Map<Delivery>(newDelivery);
            context.Add(delivery);
            var purchaseOrderDelivery = new PurchaseOrder_Delivery() {PurchaseOrderId = purchaseOrderId, DeliveryId = delivery.DeliveryId };
            context.Add(purchaseOrderDelivery);
            
            await context.SaveChangesAsync();
            
            var deliveryDTO = mapper.Map<DeliveryDTO>(delivery);
            return new CreatedAtRouteResult("GetDelivery", deliveryDTO);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] DeliveryCreationDTO updatedDelivery)
        {
            var delivery = mapper.Map<Delivery>(updatedDelivery);
            delivery.DeliveryId = id;
            context.Entry(delivery).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{deliveryId}")]
        public async Task<ActionResult> Delete(Guid deliveryId, [FromBody] Guid purchaseOrderId )
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
