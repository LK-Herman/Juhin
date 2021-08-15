﻿using AutoMapper;
using JuhinAPI.DTOs;
using JuhinAPI.Filters;
using JuhinAPI.Models;
using JuhinAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Controllers
{
    [Route("api/vendors")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly ILogger<VendorController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public VendorController(ILogger<VendorController> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        //[ResponseCache(Duration = 60)] //caching response for 60 sec
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        //[ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<VendorDTO>>> Get()
        {
            var vendors = await context.Vendors.ToListAsync();
            var vendorsDTOs = mapper.Map<List<VendorDTO>>(vendors);
            return vendorsDTOs;
        }
        
        [HttpGet("{id:Guid}", Name = "getVendor")]
        public async Task<ActionResult<VendorDTO>> GetVendorsId(Guid id)
        {
            var vendor = await context.Vendors
                .Where(v => v.VendorId == id)
                .FirstOrDefaultAsync();
            if (vendor == null)
            {
                return NotFound();
            }

            var vendorDTO = mapper.Map<VendorDTO>(vendor);

            return vendorDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VendorCreationDTO vendorCreation)
        {
            var vendor = mapper.Map<Vendor>(vendorCreation);
            context.Add(vendor);
            await context.SaveChangesAsync();
            var vendorDTO = mapper.Map<VendorDTO>(vendor);

            //return new CreatedAtRouteResult("getVendor", new { id = vendor.VendorId }, vendor);
            return new CreatedAtRouteResult("getVendor", vendorDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] VendorCreationDTO vendorCreation)
        {
            var vendor = mapper.Map<Vendor>(vendorCreation);
            vendor.VendorId = id;
            context.Entry(vendor).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public ActionResult Delete()
        {
            return NoContent();
        }

    }
}
