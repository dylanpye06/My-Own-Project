using Microsoft.AspNetCore.Mvc.Rendering;

namespace Osiansdrystonewalls.com.Models.ViewModels
{
    public class MakeABookingRequest
    {
        public string? JobName { get; set; }
        public string? Description { get; set; }

        //this is for a list howver we will only want to retieve one customer as there is only one
        // customer per job, method for only retiving a singlaur cusomer not a list? 
        public IEnumerable<SelectListItem>? Customer { get; set; }

        // collect tag

        public string? CustomerBooking { get; set; }
    }
}
