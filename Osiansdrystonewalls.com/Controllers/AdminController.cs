using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Osiansdrystonewalls.com.Data;
using Osiansdrystonewalls.com.Models;
using Osiansdrystonewalls.com.Models.Domain;
using Osiansdrystonewalls.com.Models.ViewModels;

namespace Osiansdrystonewalls.com.Controllers
{
    public class AdminController : Controller
    {
        private readonly DatabaseLinkDb databaseLinkDb;
        public AdminController(DatabaseLinkDb databaseLinkDb)
        {
            this.databaseLinkDb = databaseLinkDb;
        }

        [HttpGet]
        public IActionResult AdminLogIn() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogIn(AdminRequest adminRequest) 
        {
            var adminCheckPassword = "DryStone";
            var adminCheckEmail = "Osiancurtis0@gmail.com";

            if (adminRequest.adminEmail != adminCheckEmail)
            {
                return View("IncorrectEmail");
            }

            if(adminRequest.adminPassword != adminCheckPassword)
            {
                return View("IncorrectPassword");
            }

            else
            {
                return View("AdminPage");
            }
        }

        [HttpGet]
        [ActionName("AdminPage")]
        public IActionResult AdminPage()
        {
            return View();
        }

        [HttpPost]       
        public IActionResult AdminPage(Test test)
        {
            return View();
        }

        [HttpPost]
        [ActionName("AdminViewAccounts")]
        public async Task<IActionResult> AdminViewAccounts(CreateAccountRequest createAccountRequest)
        {
            var accounts = await databaseLinkDb.Customers.ToListAsync();
            return View(accounts);
        }

        [HttpPost]
        [ActionName("AdminViewJobs")]
        public async Task<IActionResult> AdminViewJobRequests(MakeABookingRequest makeABookingRequest)
        {      
            var jobRequests = await databaseLinkDb.JobRequests.ToListAsync();
            return View(jobRequests);
        }

        [HttpGet]
        public async Task<IActionResult> AdminEditAccounts(Guid Id)
        {
            var account = await databaseLinkDb.Customers.FirstOrDefaultAsync(x => x.Id == Id);
            if (account != null)
            {
                var adminEditAccountRequest = new AdminEditAccountRequest
                {
                    Id = account.Id,
                    FullName = account.FullName,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                    Password = account.Password,
                    PostCode = account.PostCode
                };
                return View(adminEditAccountRequest);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ActionName("AdminEditAccounts")]
        public async Task<IActionResult> AdminEditAccount(AdminEditAccountRequest adminEditAccountRequest)
        {
            var customer = new Customer
            {
                Id = adminEditAccountRequest.Id,
                FullName = adminEditAccountRequest.FullName,
                Email = adminEditAccountRequest.Email,
                PhoneNumber = adminEditAccountRequest.PhoneNumber,
                Password = adminEditAccountRequest.Password,
                PostCode = adminEditAccountRequest.PostCode
            };
            var existingAccount = await databaseLinkDb.Customers.FindAsync(customer.Id);

            if(existingAccount != null)
            {
                existingAccount.FullName = customer.FullName;
                existingAccount.Email = customer.Email;
                existingAccount.PhoneNumber = customer.PhoneNumber;
                existingAccount.Password = customer.Password;
                existingAccount.PostCode = customer.PostCode;

                await databaseLinkDb.SaveChangesAsync();

                return View("AdminPage");
            }
                return RedirectToAction("Error");                 
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(AdminEditAccountRequest adminEditAccountRequest)
        {
            var account = await databaseLinkDb.Customers.FindAsync(adminEditAccountRequest.Id);

            if(account != null) 
            { 
                databaseLinkDb.Customers.Remove(account);
                await databaseLinkDb.SaveChangesAsync();

                //show success notification

                //consider reidrect to action

                return View("AdminPage");
            }

            //show error notification

            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> AdminEditJobs(Guid Id)
        {
            var jobRequest = await databaseLinkDb.JobRequests.FirstOrDefaultAsync(x => x.Id == Id);
            if (jobRequest != null)
            {
                var adminEditJobRequest = new AdminEditJobRequest
                {
                    Id = jobRequest.Id,
                    JobName = jobRequest.JobName,
                    Description = jobRequest.Description,                 
                };
                return View(adminEditJobRequest);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ActionName("AdminEditJobs")]
        public async Task<IActionResult> AdminEditJobs(AdminEditJobRequest adminEditJobRequest)
        {
            var jobRequest = new JobRequest
            {
                Id = adminEditJobRequest.Id,
                JobName = adminEditJobRequest.JobName,
                Description = adminEditJobRequest.Description,
            };
            var existingJobRequest = await databaseLinkDb.JobRequests.FindAsync(jobRequest.Id);

            if (existingJobRequest != null)
            {
                existingJobRequest.JobName = jobRequest.JobName;
                existingJobRequest.Description = jobRequest.Description;               

                await databaseLinkDb.SaveChangesAsync();

                return View("AdminPage");
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJobRequest(AdminEditJobRequest adminEditJobRequest)
        {
            var jobRequest = await databaseLinkDb.JobRequests.FindAsync(adminEditJobRequest.Id);

            if (jobRequest != null)
            {
                databaseLinkDb.JobRequests.Remove(jobRequest);
                await databaseLinkDb.SaveChangesAsync();

                //show success notification

                //consider reidrect to action

                return View("AdminPage");
            }

            //show error notification

            return View("Error");
        }
    }
}
