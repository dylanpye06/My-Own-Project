using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Osiansdrystonewalls.com.Data;
using Osiansdrystonewalls.com.Models.Domain;
using Osiansdrystonewalls.com.Models.ViewModels;

namespace Osiansdrystonewalls.com.Controllers
{
    public class CustomerController : Controller
    {

        private readonly DatabaseLinkDb databaseLinkDb;
        public CustomerController(DatabaseLinkDb databaseLinkDb)
        {
            this.databaseLinkDb = databaseLinkDb;
        }

        [HttpGet]
        [ActionName("CreateAccount")]
        public IActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountRequest createAccountRequest)
        {
            var customer = new Customer
            {

                FullName = createAccountRequest.FullName,
                Email = createAccountRequest.Email,    //new bit of code may be why it wont work?
                PhoneNumber = createAccountRequest.PhoneNumber,
                PostCode = createAccountRequest.PostCode,
                Password = createAccountRequest.Password

            };         
                await databaseLinkDb.Customers.AddAsync(customer);
                await databaseLinkDb.SaveChangesAsync();
                return View("HomePage");         
        }

        [HttpGet]      
        public IActionResult LogIn()
        {
            return View("LogIn");
        }

        [HttpPost]
        [ActionName("LogIn")]
        public async Task<IActionResult> LogIn(LogInRequest logInRequest)
        {
            var foundCustomer = await databaseLinkDb.Customers.Where(cust => cust.Email == logInRequest.checkEmail).FirstOrDefaultAsync();

            if (foundCustomer == null)
            {
                return View("IncorrectEmail");
            }

            if (foundCustomer.Password != logInRequest.checkPassword)
            {
                return View("IncorrectPassword");
            }
            return RedirectToAction("LoggedIn",foundCustomer);
        }

        [HttpGet]
        [ActionName("LoggedIn")]
        public IActionResult LoggedIn(Customer foundCustomer)
        {
            return View(foundCustomer);
        }

        [HttpPost]
        [ActionName("LoadMakeABooking")]
        public IActionResult LoadMakeABooking()
        {
            return View("MakeABooking");
        }

        [HttpPost]
        [ActionName("MakeABooking")]
        public async Task<IActionResult> MakeABooking(MakeABookingRequest makeABookingRequest)
        {
            var jobRequest = new JobRequest
            {
                JobName = makeABookingRequest.JobName,
                Description = makeABookingRequest.Description
            };

            await databaseLinkDb.JobRequests.AddAsync(jobRequest);
            await databaseLinkDb.SaveChangesAsync();

            return RedirectToAction("LoggedIn");
        }

        [HttpGet]
        [ActionName("CustomerViewAccount")]
        public async Task<IActionResult> CustomerViewAccount(Guid id)
        {
            var foundCustomer = await databaseLinkDb.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
            
            return View("CustomerViewAccount", foundCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerEditAccount(Guid Id)
        {
            var account = await databaseLinkDb.Customers.FirstOrDefaultAsync(x => x.Id == Id);
            if (account != null)
            {
                var editAccountRequest = new AdminEditAccountRequest
                {
                    Id = account.Id,
                    FullName = account.FullName,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                    Password = account.Password,
                    PostCode = account.PostCode
                };
                return View(editAccountRequest);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ActionName("CustomerEditAccount")]
        public async Task<IActionResult> CustomerEditAccount(AdminEditAccountRequest editAccountRequest)
        {
            var customer = new Customer
            {
                Id = editAccountRequest.Id,
                FullName = editAccountRequest.FullName,
                Email = editAccountRequest.Email,
                PhoneNumber = editAccountRequest.PhoneNumber,
                Password = editAccountRequest.Password,
                PostCode = editAccountRequest.PostCode
            };
            var existingAccount = await databaseLinkDb.Customers.FindAsync(customer.Id);

            if (existingAccount != null)
            {
                existingAccount.FullName = customer.FullName;
                existingAccount.Email = customer.Email;
                existingAccount.PhoneNumber = customer.PhoneNumber;
                existingAccount.Password = customer.Password;
                existingAccount.PostCode = customer.PostCode;

                await databaseLinkDb.SaveChangesAsync();

                return RedirectToAction("LoggedIn");
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        [ActionName("CustomerViewBooking")]
        public async Task<IActionResult> CustomerViewBooking(Guid id)
        {
            var foundBooking = await databaseLinkDb.JobRequests.Where(x => x.Id == id).FirstOrDefaultAsync();

            return View("CustomerViewBooking", foundBooking);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerEditBooking(Guid Id)
        {
            var jobRequest = await databaseLinkDb.JobRequests.FirstOrDefaultAsync(x => x.Id == Id);
            if (jobRequest != null)
            {
                var editJobRequest = new AdminEditJobRequest
                {
                    Id = jobRequest.Id,
                    JobName = jobRequest.JobName,
                    Description = jobRequest.Description,
                };
                return View(editJobRequest);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ActionName("CustomerEditAccount")]
        public async Task<IActionResult> CustomerEditBooking(AdminEditJobRequest editJobRequest)
        {
            var jobRequest = new AdminEditJobRequest
            {
                Id = editJobRequest.Id,
                JobName = editJobRequest.JobName,
                Description = editJobRequest.Description,
            };
            var existingBooking = await databaseLinkDb.JobRequests.FindAsync(jobRequest.Id);

            if (existingBooking != null)
            {
                existingBooking.JobName = jobRequest.JobName;
                existingBooking.Description = jobRequest.Description;

                await databaseLinkDb.SaveChangesAsync();

                return RedirectToAction("LoggedIn");
            }
            return RedirectToAction("Error");
        }
    }
}
