using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Osiansdrystonewalls.com.Data;
using Osiansdrystonewalls.com.Models;
using Osiansdrystonewalls.com.Models.Domain;
using Osiansdrystonewalls.com.Models.ViewModels;
using Osiansdrystonewalls.com.Repositories;


namespace Osiansdrystonewalls.com.Controllers
{
    public class AdminController(IAccountRepository accountRepository, IBookingRepository bookingRepository) : Controller
    {
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

            if (adminRequest.AdminEmail != adminCheckEmail)
            {
                return View("IncorrectEmail");
            }

            if(adminRequest.AdminPassword != adminCheckPassword)
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
        [ActionName("AdminViewAccounts")]
        public async Task<IActionResult> AdminViewAccounts()
        {
            var accounts = await accountRepository.GetAllASync();
            return View(accounts);
        }

        [HttpPost]
        [ActionName("AdminViewJobs")]
        public async Task<IActionResult> AdminViewJobRequests()
        {
            var jobRequests = await bookingRepository.GetAllASync();
            return View(jobRequests);
        }

        [HttpGet]
        public async Task<IActionResult> AdminEditAccounts(Guid Id)
        {
            var account = await accountRepository.GetASync(Id);

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
            await accountRepository.UpdateASync(customer);
            return RedirectToAction("AdminPage");                 
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(AdminEditAccountRequest adminEditAccountRequest)
        {
            var account = await accountRepository.DeleteASync(adminEditAccountRequest.Id);

            if(account != null) 
            { 
                //show success notification
                return View("AdminPage");
            }
            //show error notification
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> AdminEditJobs(Guid Id)
        {
            var jobRequest = await bookingRepository.GetASync(Id);
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
            await bookingRepository.UpdateASync(jobRequest);
            return RedirectToAction("AdminPage");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJobRequest(AdminEditJobRequest adminEditJobRequest)
        {
            var jobRequest = await bookingRepository.DeleteASync(adminEditJobRequest.Id);

            if (jobRequest != null)
            {
                //show success notification
                return View("AdminPage");
            }
            //show error notification
            return View("Error");
        }
    }
}
