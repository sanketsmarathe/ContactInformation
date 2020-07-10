using System;
using System.ComponentModel.DataAnnotations;

namespace EvolentTest.Common
{
    public class ContactModel
    {
        public Guid ContactId { get; set; }
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
