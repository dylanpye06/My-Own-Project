using Microsoft.EntityFrameworkCore;
using Osiansdrystonewalls.com.Models;
using Osiansdrystonewalls.com.Models.Domain;

namespace Osiansdrystonewalls.com.Data
{
    public class DatabaseLinkDb(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<JobRequest> JobRequests { get; set; }

    }
}
