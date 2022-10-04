using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CustomerManagementSystem.Models
{
    public partial class Address
    {
        public Address()
        {
            Customer = new HashSet<Customer>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public string Province { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
