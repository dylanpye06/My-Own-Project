using Osiansdrystonewalls.com.Models.Domain;

namespace Osiansdrystonewalls.com.Models.ViewModels
{
    public class AdminEditJobRequest
    {
        public Guid Id { get; set; }
        public string? JobName { get; set; }
        public string? Description { get; set; }
        public Customer? Customer { get; set; }
    }
}
