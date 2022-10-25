using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CustomerManagementSystem.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [DisplayName("Phone number")]
        public int PhoneNumber { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }

    
    }
}
