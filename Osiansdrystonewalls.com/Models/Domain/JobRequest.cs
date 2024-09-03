using System.Reflection;

namespace Osiansdrystonewalls.com.Models.Domain
{
    public class JobRequest
    {
        public Guid Id { get; set; }
        public string? JobName { get; set; }
        public string? Description { get; set; }
        public ICollection<Customer>? Customer { get; set; }   
    }
}
