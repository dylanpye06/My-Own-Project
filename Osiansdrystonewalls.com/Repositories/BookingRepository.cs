using Microsoft.EntityFrameworkCore;
using Osiansdrystonewalls.com.Data;
using Osiansdrystonewalls.com.Models.Domain;

namespace Osiansdrystonewalls.com.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DatabaseLinkDb databaseLinkDb;
        public BookingRepository(DatabaseLinkDb databaseLinkDb)
        {
            this.databaseLinkDb = databaseLinkDb;
        }

        public async Task<JobRequest> AddASync(JobRequest jobRequest)
        {

            await databaseLinkDb.JobRequests.AddAsync(jobRequest);
            await databaseLinkDb.SaveChangesAsync();
            return jobRequest;
        }

        public async Task<JobRequest?> DeleteASync(Guid id)
        {
            var existingBooking = await databaseLinkDb.JobRequests.FindAsync(id);

            if (existingBooking != null)
            {
                databaseLinkDb.JobRequests.Remove(existingBooking);
                await databaseLinkDb.SaveChangesAsync();
                return existingBooking;
            }
            return null;
        }

        public async Task<IEnumerable<JobRequest>> GetAllASync()
        {
            return await databaseLinkDb.JobRequests.ToListAsync();
        }

        public Task<JobRequest?> GetASync(Guid id)
        {
            return databaseLinkDb.JobRequests.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<JobRequest?> UpdateASync(JobRequest jobRequest)
        {
            var booking = await databaseLinkDb.JobRequests.FindAsync(jobRequest.Id);

            if (booking != null)
            {
                booking.JobName = booking.JobName;
                booking.Description = booking.Description;

                await databaseLinkDb.SaveChangesAsync();
                return booking;
            }

            return null;
        }
    }
}
