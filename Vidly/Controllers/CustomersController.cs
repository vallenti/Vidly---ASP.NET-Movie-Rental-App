﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext context;

        public CustomersController()
        {
            this.context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            this.context.Dispose();
        }
        public ActionResult Index()
        {
            var viewModel = new CustomersViewModel()
            {
                Customers = new List<Customer>(context.Customers.Include(c => c.MembershipType))
            };
            return View(viewModel);
        }
        public ActionResult New()
        {
            var membershipTypes = context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel()
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id == 0)
            {
                context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = context.Customers.Find(customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthday = customer.Birthday;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.Name = customer.Name;
                //TODO map other properties

            }
            context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = context.MembershipTypes.ToList()
            };
            return View("CustomerForm", viewModel);
        }

        [Route("customers/details/{id}")]
        public ActionResult Details(int id)
        {
            var customer = context.Customers
                .Include(c => c.MembershipType)
                .FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new DetailsCustomerViewModel()
            {
                Name = customer.Name,
                MembershipType = customer.MembershipType.Name
            };
            if (customer.Birthday.HasValue)
            {
                viewModel.Birthdate = $"{customer.Birthday.Value.Day}/{customer.Birthday.Value.Month}/{customer.Birthday.Value.Day}";
            }

            return View(viewModel);
        }
    }
}