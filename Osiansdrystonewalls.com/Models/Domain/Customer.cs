using Osiansdrystonewalls.com.Models.Domain;

namespace Osiansdrystonewalls.com.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string PostCode { get; set; }
        public required string Password { get; set; }
        public ICollection<JobRequest>? JobRequests { get; set; }
    }
}
