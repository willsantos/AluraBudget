using System;
using System.ComponentModel.DataAnnotations;

namespace AluraBudget.Models
{
    public class Outgoing
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTime Date { get; set; }

    }
}
