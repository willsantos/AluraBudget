using System;
using System.ComponentModel.DataAnnotations;

namespace AluraBudget.Data.DTO.IncomeDto
{
    public class UpdateIncomeDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public Decimal Value { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
