using System;
using System.ComponentModel.DataAnnotations;

namespace AluraBudget.Data.DTO.IncomeDto
{
    public class CreateIncomeDto
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Value { get; set; }

        public DateTime Date { get; set; }
    }
}
