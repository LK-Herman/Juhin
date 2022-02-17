using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.DTOs
{
    public class UserDetailsDTO:UserDTO
    {
        public bool IsAdmin { get; set; }
        public bool IsSpecialist { get; set; }
        public bool IsWarehouseman { get; set; }
        public bool IsGuest { get; set; }

    }
}
