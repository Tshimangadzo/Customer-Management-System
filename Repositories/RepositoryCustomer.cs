using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerManagementSystem.Interfaces;

namespace CustomerManagementSystem.Repositories
{
    public class RepositoryCustomer : ICustomer
    {
        List<Models.Customer> ICustomer.Create(Models.Customer customer)
        {
            throw new NotImplementedException();
        }

        Models.Customer ICustomer.Delete(string id)
        {
            throw new NotImplementedException();
        }

        Models.Customer ICustomer.Read(string id)
        {
            throw new NotImplementedException();
        }

        List<Models.Customer> ICustomer.Read()
        {
            throw new NotImplementedException();
        }

        List<Models.Customer> ICustomer.Update(Models.Customer customer, string id)
        {
            throw new NotImplementedException();
        }
    }


}
