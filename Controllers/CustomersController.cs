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
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using CustomerManagementSystem.pagination;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using LumenWorks.Framework.IO.Csv;
using System.Data;
using CustomerManagementSystem.Support;
using System.Text;
using ExcelDataReader;

namespace CustomerManagementSystem.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomer _repositoryCustomer;
        private readonly IConfiguration _configuration;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

    

        [Obsolete]
        public CustomersController(ICustomer repositoryCustomer,
                                   IConfiguration configuration,IHostingEnvironment hostingEnvironment)
        {

            _repositoryCustomer = repositoryCustomer;
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
      
        [HttpPost]
        [Obsolete]
        public List<Customer> ProcessCsv(IFormFile file)
        {
            List<Customer> customers = new List<Customer>();
            if (file != null)
            {
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");
                string fileName = Path.GetFileName(file.FileName);
                bool isFileSaved = SaveFile(file, path, fileName);

                if (isFileSaved)
                {
                    customers = GetCustomers(path, fileName);
                }
            }

            return customers;
        }


        // GET: Customers
        
        public ViewResult Index(string ColunmName,
                                string sortOrder, 
                                string SearchColunmName, 
                                string searchValue, 
                                int? pageIndex,
                                List<Customer> _customers)
        {

            // string sortOrder,
            // string currentFilter, string searchString, int? pageIndex
            List<Customer> customers;

            if (_customers.Count() > 0)
            {
                ViewBag.isClicked = true;
                ViewBag.uploaded = true;
                customers = _customers;
            }
            else
            {


                if (!String.IsNullOrEmpty(ColunmName) && String.IsNullOrEmpty(SearchColunmName))
                {
                    sortOrder = sortOrder == null ? "asc" : sortOrder == "asc" ? "desc" : "asc";
                    customers = _repositoryCustomer.SortCustomers(ColunmName, sortOrder);
                }
                else if (!String.IsNullOrEmpty(SearchColunmName) && !String.IsNullOrEmpty(searchValue))
                {
                    customers = _repositoryCustomer.SearchCustomers(SearchColunmName, searchValue);
                }
                else
                {
                    customers = _repositoryCustomer.Read();
                }
            }
            ViewData["sortOrder"] = sortOrder;
            ViewData["ColunmName"] = ColunmName;
            ViewBag.uploaded = false;
            var pageSize = _configuration.GetValue("PageSize", 4);
            //  customers = await PaginatedList<Customer>.CreateAsync(customers, pageIndex ?? 1, pageSize);

            return View(customers);

        }

        
        [Route("Customers/processCsv")]
        [Obsolete]
        [HttpPost]
        public ViewResult GetCustomers(IFormFile file)
        {
            List<Customer> customers;
            if (file != null)
            {
                customers = ProcessCsv(file);
               
            }
            else
            {
                customers = new List<Customer>();
            }

            return Index(null,null,null,null,0,customers);
        }

     

        
        public IActionResult CreateCustomers(List<Customer> customers)
        {
            ViewData["customers"] = customers;
            return View(customers);
        }

        private static List<Customer> GetCustomers(string path,string fileName) {
                List<Customer> customers = new List<Customer>();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var stream = System.IO.File.Open(Path.Combine(path, fileName), FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                    int createId = 0;
                        while (reader.Read())
                    {
                        if (reader.GetValue(0) != null && 
                            reader.GetValue(1) != null && 
                            reader.GetValue(2) != null && 
                            reader.GetValue(3) != null)
                        {
                            if (reader.GetValue(3).GetType().Name != "String")
                            {
                                customers.Add(new Customer
                                {
                                    Id = createId,
                                    Name = reader.GetValue(0).ToString(),
                                    Surname = reader.GetValue(1).ToString(),
                                    Email = reader.GetValue(2).ToString(),
                                    PhoneNumber = Convert.ToInt32(reader.GetValue(3))
                                });
                                createId++;
                            }

                        }
                        else {
                            break;
                        }
                    }
                }
                }

            return customers;
        }

        private static bool SaveFile(IFormFile file,string path,string fileName) {
        
            bool isFileSaved = false;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            List<string> uploadedFiles = new List<string>();

            
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
                uploadedFiles.Add(fileName);
                isFileSaved = true;
            }
            return isFileSaved;

        }

       

        // GET: Customers/Details/5
        
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
        
        public IActionResult Create()
        {
         
            return View();
        }
       


        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create([Bind("Name,Surname,Email,PhoneNumber,AddressId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var _customer = _repositoryCustomer.Create(customer);

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        
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
        
        public IActionResult DeleteConfirmed(int id)
        {

            var customer = _repositoryCustomer.Delete(id);
            return RedirectToAction(nameof(Index));

        }
        
        private bool CustomerExists(int id)
        {
            return _repositoryCustomer.ReadById(id).Id == id;
        }
    }
}
