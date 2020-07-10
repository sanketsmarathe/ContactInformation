using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvolentTest.Database.Entities
{
    public class Contact : BaseEntity
    {
        [Key]
        public Guid ContactId { get; set; }

        [ForeignKey("User")]
        public string Id { get; set; }
        public string Status { get; set; }
        public ApplicationUser User { get; set; }
    }
}
