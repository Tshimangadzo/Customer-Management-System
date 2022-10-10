using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Interfaces;
using CustomerManagementSystem.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;

namespace CustomerManagementSystem.Repositories
{
    public class RepositoryCustomer : ICustomer
    {

        private readonly CustomerManagementSystemContext _context;

        public RepositoryCustomer(CustomerManagementSystemContext context) {
            _context = context;
        }

        public int Create(Customer customer)
        {
            return  _context.Database.ExecuteSqlRaw("exec sp_create_customer @Name,@Surname,@Email,@PhoneNumber", 
                new SqlParameter("Name", customer.Name),
                new SqlParameter("Surname", customer.Surname),
                new SqlParameter("Email", customer.Email),
                new SqlParameter("PhoneNumber", customer.PhoneNumber));
        }

        public int Delete(int id)
        {
            return _context.Database.ExecuteSqlRaw("exec sp_delete @Id ",new SqlParameter("Id", id));
        }

        public List<Customer> Read(int? id)
        {

            List<Customer> customers = new List<Customer>();
            try
            {
                if (id != null)
                {
                    customers = _context.Customer.FromSqlRaw("exec sp_customer @Id ", new SqlParameter("Id", id)).ToList();
                }
                else
                {
                    customers = _context.Customer.FromSqlRaw("exec sp_customers").ToList();
                }
                return customers;
            }
            catch (Exception e) {
                //Log error here (e)

                return customers;
            }
        
        }

        public List<Customer> SortCustomers(string ColunmName, string type)
        {
            var customers = _context.Customer.FromSqlRaw("exec sp_customers_ordered @ColunmName,@type ", new SqlParameter("ColunmName", ColunmName), new SqlParameter("type", type)).ToList();
            return customers;
        }

        int ICustomer.Update(Customer customer, int id)
        {
           var customer_ = _context.Database.ExecuteSqlRaw("exec sp_update @Id, @Name,@Surname,@Email,@PhoneNumber",
              new SqlParameter("Id", id),
              new SqlParameter("Name", customer.Name),
              new SqlParameter("Surname", customer.Surname),
              new SqlParameter("Email", customer.Email),
              new SqlParameter("PhoneNumber", customer.PhoneNumber));
            return customer_;
        }
    }
}
