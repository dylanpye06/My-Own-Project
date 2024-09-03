using Microsoft.AspNetCore.Mvc.Rendering;
using Osiansdrystonewalls.com.Models.Domain;

namespace Osiansdrystonewalls.com.Models.ViewModels
{
    public class MakeABookingRequest
    {
        public string? JobName { get; set; }
        public string? Description { get; set; }

        //this is for a list howver we will only want to retieve one customer as there is only one
        // customer per job, method for only retiving a singlaur cusomer not a list? 
        public Customer? Customer { get; set; }
    }
}
