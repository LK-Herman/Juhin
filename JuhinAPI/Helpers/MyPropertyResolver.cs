using AutoMapper;
using JuhinAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Helpers
{
    public class MyPropertyResolver: IValueResolver<IdentityUser, UserDetailsDTO, string>
    {
        private readonly UserManager<IdentityUser> userManager;

        public MyPropertyResolver(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public string Resolve(IdentityUser source, UserDetailsDTO destination, string destMember, ResolutionContext context)
        {
            var value = "";
            if (destMember == "IsAdmin") value = "Admin";
            if (destMember == "IsGuest") value = "Guest";
            if (destMember == "IsWarehouseman") value = "Warehouseman";
            if (destMember == "IsSpecialist") value = "Specialist";

            return userManager.IsInRoleAsync(source, value ).Result.ToString();
        }
    }
}
