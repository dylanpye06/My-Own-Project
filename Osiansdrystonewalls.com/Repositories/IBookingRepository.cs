using Osiansdrystonewalls.com.Models.Domain;

namespace Osiansdrystonewalls.com.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<JobRequest>> GetAllASync();

        Task<JobRequest?> GetASyncCustomer(Guid Id);

        Task<JobRequest> AddASync(JobRequest jobRequest);

        Task<JobRequest?> UpdateASyncAdmin(JobRequest jobRequest);

        Task<JobRequest?> UpdateASyncCustomer(JobRequest jobRequest, Guid CustomerId);

        Task<JobRequest?> DeleteASyncAdmin(Guid id);
        Task<JobRequest?> DeleteASyncCustomer(Guid CustomerId);

        Task<JobRequest?> GetASyncAdmin(Guid Id);
    }
}

