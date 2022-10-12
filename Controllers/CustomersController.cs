using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using CustomerManagementSystem.Repositories;
using CustomerManagementSystem.Interfaces;

namespace CustomerManagementSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomer _repositoryCustomer;

        public CustomersController(ICustomer repositoryCustomer)
        {

            _repositoryCustomer = repositoryCustomer;
        }

        // GET: Customers
        [Authorize]
        public IActionResult Index(string ColunmName, string TypeAction,string SearchColunmName,string searchValue)
        {
            List<Customer> customers;

            if (String.IsNullOrEmpty(ColunmName) && String.IsNullOrEmpty(TypeAction) && String.IsNullOrEmpty(SearchColunmName))
            {
                customers = _repositoryCustomer.Read();
            }
            else if (!String.IsNullOrEmpty(ColunmName) && String.IsNullOrEmpty(SearchColunmName))
            {
                TypeAction = TypeAction == null ? "asc" : TypeAction == "asc"?"desc":"asc";
                customers = _repositoryCustomer.SortCustomers(ColunmName, TypeAction);
            }
            else{
                customers = _repositoryCustomer.SearchCustomers(SearchColunmName, searchValue);
            }
            ViewData["TypeAction"] = TypeAction;
            ViewData["ColunmName"] = ColunmName;
            return View(customers);
        }

        // GET: Customers/Details/5
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer customer = _repositoryCustomer.ReadById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        [Authorize]
        public IActionResult Create()
        {
         
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,PhoneNumber,AddressId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var _customer = _repositoryCustomer.Create(customer);

                return RedirectToAction(nameof(Index));
            }
 
            return View(customer);
        }

        // GET: Customers/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _repositoryCustomer.ReadById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(int id, [Bind("Id,Name,Surname,Email,PhoneNumber,AddressId")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repositoryCustomer.Update(customer, id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Customers/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Customer customer = _repositoryCustomer.ReadById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(int id)
        {

            var customer = _repositoryCustomer.Delete(id);
            return RedirectToAction(nameof(Index));

        }
        [Authorize]
        private bool CustomerExists(int id)
        {
            return _repositoryCustomer.ReadById(id).Id == id;
        }
    }
}
