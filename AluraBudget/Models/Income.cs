using System;
using System.ComponentModel.DataAnnotations;

namespace AluraBudget.Models
{
    public class Income
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Decimal Value { get; set; }

        public DateTime Date { get; set; }

    }
}
