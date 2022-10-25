using System;
using System.Collections.Generic;
using System.Linq;
using CustomerManagementSystem.Interfaces;
using CustomerManagementSystem.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            int IntCustomer;
            try
            {
               IntCustomer =  _context.Database.ExecuteSqlRaw("exec sp_create_customer @Name,@Surname,@Email,@PhoneNumber",
                    new SqlParameter("Name", customer.Name),
                    new SqlParameter("Surname", customer.Surname),
                    new SqlParameter("Email", customer.Email),
                    new SqlParameter("PhoneNumber", customer.PhoneNumber));
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return IntCustomer;

        }

        public int Delete(int? id)
        {
            int IntCustomer;

            try {
                IntCustomer = _context.Database.ExecuteSqlRaw("exec sp_delete @Id ", new SqlParameter("Id", id));
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return IntCustomer;
        }



        public List<Customer> Read()
        {
            List<Customer> customers;
            try
            {
                customers = _context.Customer.FromSqlRaw("exec sp_customers").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;

        }

        public Customer ReadById(int? id)
        {
            List<Customer> customers;
            try
            {
                customers = _context.Customer.FromSqlRaw("exec sp_customer @Id ", new SqlParameter("Id", id)).ToList();
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers.FirstOrDefault();
        }

        public List<Customer> SearchCustomers(string ColunmName, string value)
        {
            List<Customer> customers;
            try
            {
                customers = _context.Customer.FromSqlRaw("exec sp_customers_search @ColunmName, @Value ", new SqlParameter("ColunmName", ColunmName), new SqlParameter("Value", value)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }

        public List<Customer> SortCustomers(string ColunmName, string type)
        {
            List<Customer> customers;
            try {
                customers = _context.Customer.FromSqlRaw("exec sp_customers_ordered @ColunmName,@type ", new SqlParameter("ColunmName", ColunmName), new SqlParameter("type", type)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }

        int ICustomer.Update(Customer customer, int id)
        {
            int IntCustomer;
            try
            {
                IntCustomer = _context.Database.ExecuteSqlRaw("exec sp_update @Id, @Name,@Surname,@Email,@PhoneNumber",
                   new SqlParameter("Id", id),
                   new SqlParameter("Name", customer.Name),
                   new SqlParameter("Surname", customer.Surname),
                   new SqlParameter("Email", customer.Email),
                   new SqlParameter("PhoneNumber", customer.PhoneNumber));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return IntCustomer;
        }
    }
}
