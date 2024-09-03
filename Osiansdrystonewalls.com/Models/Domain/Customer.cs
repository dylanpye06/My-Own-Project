using Osiansdrystonewalls.com.Models.Domain;
using System.Reflection;


namespace Osiansdrystonewalls.com.Models.Domain
{
    public class Customer
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string PostCode { get; set; }
        public required string Password { get; set; }
    }
}
