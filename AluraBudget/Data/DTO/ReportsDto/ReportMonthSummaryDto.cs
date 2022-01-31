

using AluraBudget.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AluraBudget.Data.DTO.ReportsDto
{
    public class ReportMonthSummaryDto
    {
        [Required]
        public decimal TotalIncomes { get; set; }
        
        [Required]
        public decimal TotalOutgoings { get; set; }

        [Required]
        public decimal Balance { get; set; }

        public ICollection<ReportMonthSummaryByCategoryDto> TotalCategory 
        { get; set; }

    }

    public class ReportMonthSummaryByCategoryDto
    {
        [Required]
        public OutgoingCategory Category { get; set; }

        [Required]
        public decimal TotalOutgoings { get; set; }
    }
}
