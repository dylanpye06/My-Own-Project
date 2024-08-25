using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Osiansdrystonewalls.com.Data;
using Osiansdrystonewalls.com.Models.Domain;
using Osiansdrystonewalls.com.Models.ViewModels;
using Osiansdrystonewalls.com.Repositories;

namespace Osiansdrystonewalls.com.Controllers
{
    public class CustomerController(IAccountRepository accountRepository, IBookingRepository bookingRepository) : Controller
    {
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
                Email = createAccountRequest.Email, 
                PhoneNumber = createAccountRequest.PhoneNumber,
                PostCode = createAccountRequest.PostCode,
                Password = createAccountRequest.Password
            };

            var checkEmail = await accountRepository.CreateAccountCheck(createAccountRequest);

            if (checkEmail != null)
            {
                return View("Error");
            }

            await accountRepository.AddASync(customer);
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
            var foundCustomer1 = await accountRepository.GetAccountSync(logInRequest);

            if (foundCustomer1 != null)
            {
                var customerID = foundCustomer1.Id;
                var foundCustomer = await accountRepository.GetASync(customerID);

                if (foundCustomer1 == null)
                {
                    return View("IncorrectEmail");
                }

                if (foundCustomer1.Password != logInRequest.CheckPassword)
                {
                    return View("IncorrectPassword");
                }
                return RedirectToAction("LoggedIn", foundCustomer);
            }
            else
                return View("Error");
        }

        [HttpGet]
        [ActionName("LoggedIn")]
        public async Task<IActionResult> LoggedIn(Guid id)
        {
            var foundCustomer1 = await accountRepository.GetASync(id);

            return View(foundCustomer1);
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
            await bookingRepository.AddASync(jobRequest);
            return RedirectToAction("LoggedIn");
        }

        [HttpGet]
        [ActionName("CustomerViewAccount")]
        public async Task<IActionResult> CustomerViewAccount(Guid id)
        {
            var foundCustomer = await accountRepository.GetASync(id);
            return View(foundCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerEditAccount(Guid Id)
        {
            var account = await accountRepository.GetASync(Id);

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
        public async Task<IActionResult> CustomerEditAccount(AdminEditAccountRequest editAccountRequest, Guid id)
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
            var existingAccount = accountRepository.GetASync(id);
            await accountRepository.UpdateASync(customer);
            return RedirectToAction("LoggedIn", existingAccount);
        }

        [HttpGet]
        [ActionName("CustomerViewBooking")]
        public async Task<IActionResult> CustomerViewBooking(Guid id)
        {
            var foundBooking = await bookingRepository.GetASync(id);

            return View("CustomerViewBooking", foundBooking);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerEditBooking(Guid Id)
        {
            var jobRequest = await bookingRepository.GetASync(Id);

            if(jobRequest != null)
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
        [ActionName("CustomerEditBooking")]
        public async Task<IActionResult> CustomerEditBooking(AdminEditJobRequest editJobRequest)
        {
            var jobRequest = new JobRequest
            {
                Id = editJobRequest.Id,
                JobName = editJobRequest.JobName,
                Description = editJobRequest.Description,
            };
            await bookingRepository.UpdateASync(jobRequest);

            return RedirectToAction("LoggedIn");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccountRequest(AdminEditAccountRequest adminEditAccountRequest)
        {
            var account = await accountRepository.DeleteASync(adminEditAccountRequest.Id);

            if (account != null)
            {
                //show success notification
                return View("HomePage");
            }
            //show error notification
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJobRequest(AdminEditJobRequest adminEditJobRequest)
        {
            var jobRequest = await bookingRepository.DeleteASync(adminEditJobRequest.Id);

            if (jobRequest != null)
            {
                //show success notification
                return View("LoggedIn");
            }
            //show error notification
            return View("Error");
        }
    }
}
