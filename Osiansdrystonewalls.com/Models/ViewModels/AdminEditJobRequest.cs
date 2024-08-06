namespace Osiansdrystonewalls.com.Models.ViewModels
{
    public class AdminEditJobRequest
    {
        public Guid Id { get; set; }
        public required string JobName { get; set; }
        public required string Description { get; set; }
    }
}
