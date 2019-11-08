using System.ComponentModel.DataAnnotations;

namespace UmbracoSite.Models
{
    public class ContactModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}