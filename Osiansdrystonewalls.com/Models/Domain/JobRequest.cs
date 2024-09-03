using System.Reflection;
using Osiansdrystonewalls.com.Models.Domain;

namespace Osiansdrystonewalls.com.Models.Domain
{
    public class JobRequest
    {
        public Guid Id { get; set; }
        public string? JobName { get; set; }
        public string? Description { get; set; }
        public Customer? Customer { get; set; }   
    }
}
