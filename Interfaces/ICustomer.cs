using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Models;


namespace CustomerManagementSystem.Interfaces
{
    public interface ICustomer
    {

        public List<Customer> Create(Customer customer);
        public List<Customer> Update(Customer customer, string id);
        public Customer Read(string id);
        public List<Customer> Read();
        public Customer Delete(string id);
    }

  
}
