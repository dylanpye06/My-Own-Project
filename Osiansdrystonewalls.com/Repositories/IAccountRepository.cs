using Osiansdrystonewalls.com.Models.Domain;
using Osiansdrystonewalls.com.Models.ViewModels;

namespace Osiansdrystonewalls.com.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Customer>> GetAllASync();

        Task<Customer?> GetASync(Guid id);

        Task<Customer?> GetAccountSync(LogInRequest logInRequest);

        Task <Customer> AddASync(Customer customer);

        Task <Customer?> UpdateASync(Customer customer);

        Task <Customer?> DeleteASync(Guid id);

        Task<Customer?> CreateAccountCheck(CreateAccountRequest createAccountRequest);
    }
}
