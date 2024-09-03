using Azure;
using Microsoft.EntityFrameworkCore;
using Osiansdrystonewalls.com.Data;
using Osiansdrystonewalls.com.Models.Domain;
using Osiansdrystonewalls.com.Models.ViewModels;

namespace Osiansdrystonewalls.com.Repositories
{
    public class AccountRepository(DatabaseLinkDb databaseLinkDb) : IAccountRepository
    {
        public async Task<Customer> AddASync(Customer customer)
        {
            await databaseLinkDb.Customers.AddAsync(customer);
            await databaseLinkDb.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> DeleteASync(Guid id)
        {
            var existingAccount = await databaseLinkDb.Customers.FindAsync(id);

            if (existingAccount != null)
            {
                databaseLinkDb.Customers.Remove(existingAccount);
                await databaseLinkDb.SaveChangesAsync();
                return existingAccount;
            }
            return null;
        }

        public async Task<IEnumerable<Customer>> GetAllASync()
        {
            return await databaseLinkDb.Customers.ToListAsync();
        }

        public async Task<Customer?> GetASync(Guid id)
        {
            return await databaseLinkDb.Customers.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Customer?> GetAccountSync (LogInRequest logInRequest)
        {
            return await databaseLinkDb.Customers.Where(cust => cust.Email == logInRequest.CheckEmail).FirstOrDefaultAsync();
        }

        public async Task<Customer?> UpdateASync(Customer customer)
        {
            var account = await databaseLinkDb.Customers.FindAsync(customer.Id);

            if (account != null)
            {
                account.FullName = customer.FullName;
                account.Email = customer.Email;
                account.PhoneNumber = customer.PhoneNumber;
                account.PostCode = customer.PostCode;
                account.Password = customer.Password;

                await databaseLinkDb.SaveChangesAsync();
                return account;
            }
            return null;
        }

        public async Task<Customer?> CreateAccountCheck(CreateAccountRequest createAccountRequest)
        {
            return await databaseLinkDb.Customers.Where(cust => cust.Email == createAccountRequest.Email).FirstOrDefaultAsync();
        }
    }
}
