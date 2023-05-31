using Microsoft.AspNetCore.Mvc.Rendering;

namespace DentalCenter.Models
{
    public class ContactViewModel
    {
        public int ContactId { get; set; }
        public string ContactSurname { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMessage { get; set; }
        public SelectListItem Emps { get; set; }
    }
}
