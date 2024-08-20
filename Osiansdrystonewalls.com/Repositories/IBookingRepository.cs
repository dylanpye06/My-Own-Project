using Osiansdrystonewalls.com.Models.Domain;

namespace Osiansdrystonewalls.com.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<JobRequest>> GetAllASync();

        Task<JobRequest?> GetASync(Guid id);

        Task<JobRequest> AddASync(JobRequest jobRequest);

        Task<JobRequest?> UpdateASync(JobRequest jobRequest);

        Task<JobRequest?> DeleteASync(Guid id);
    }
}

