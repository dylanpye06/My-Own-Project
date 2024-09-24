using Microsoft.EntityFrameworkCore;
using Osiansdrystonewalls.com.Data;
using Osiansdrystonewalls.com.Models.Domain;
using System.Linq;

namespace Osiansdrystonewalls.com.Repositories
{
    public class BookingRepository(DatabaseLinkDb databaseLinkDb) : IBookingRepository
    {
        private readonly DatabaseLinkDb databaseLinkDb = databaseLinkDb;

        public async Task<JobRequest> AddASync(JobRequest jobRequest)
        {
            await databaseLinkDb.JobRequests.AddAsync(jobRequest);
            await databaseLinkDb.SaveChangesAsync();
            return jobRequest;
        }

        public async Task<JobRequest?> DeleteASyncCustomer(Guid CustomerId)
        {
            var existingBooking = await databaseLinkDb.JobRequests.Include(x => x.Customer).Where(x => x.Customer.Id == CustomerId).FirstOrDefaultAsync();

            if (existingBooking != null)
            {
                databaseLinkDb.JobRequests.Remove(existingBooking);
                await databaseLinkDb.SaveChangesAsync();
                return existingBooking;
            }
            return null;
        }

        public async Task<JobRequest?> DeleteASyncAdmin(Guid id)
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
            return await databaseLinkDb.JobRequests.Include(x => x.Customer).ToListAsync();
        }

        public async Task<JobRequest?> GetASyncCustomer(Guid CustomerId)
        {        
                return await databaseLinkDb.JobRequests.Include(x => x.Customer).Where(x => x.Customer.Id == CustomerId).FirstOrDefaultAsync();
        }

        public async Task<JobRequest?> GetASyncAdmin(Guid Id)
        {
            return await databaseLinkDb.JobRequests.Include(x => x.Customer).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<JobRequest?> UpdateASyncAdmin(JobRequest jobRequest)
        {
            var booking = await databaseLinkDb.JobRequests.FindAsync(jobRequest.Id);

            if (booking != null)
            {
                booking.JobName = jobRequest.JobName;
                booking.Description = jobRequest.Description;

                await databaseLinkDb.SaveChangesAsync();
                return booking;
            }
            return null;
        }

        public async Task<JobRequest?> UpdateASyncCustomer(JobRequest jobRequest, Guid CustomerId)
        {
            var booking = await databaseLinkDb.JobRequests.Include(x => x.Customer).Where(x => x.Customer.Id == CustomerId).FirstOrDefaultAsync();

            if (booking != null)
            {
                booking.JobName = jobRequest.JobName;
                booking.Description = jobRequest.Description;

                await databaseLinkDb.SaveChangesAsync();
                return booking;
            }
            return null;
        }
    }
}
