using AluraBudget.Data.DTO.ReportsDto;
using AluraBudget.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AluraBudget.Controllers
{
    [ApiController]
    [Route("/resumo")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportsService _reportsService;

        public ReportsController(ReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        [HttpGet("{year}/{month}")]
        [Authorize(Roles = "regular")]
        public IActionResult Index(int year, int month)
        {

            ReportMonthSummaryDto readDto =
                _reportsService.GetSummaryByCategory(year,month);

            if(readDto != null) return Ok(readDto);
            return NoContent();
        }
    }
}
