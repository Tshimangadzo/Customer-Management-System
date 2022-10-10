using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Models;


namespace CustomerManagementSystem.Interfaces
{
    public interface ICustomer
    {

        public int Create(Customer customer);
        public int Update(Customer customer, int id);
        public  List<Customer>Read(int? id);
        public int Delete(int id);
        public List<Customer> SortCustomers(string ColunmName, string type );
    }

  
}
