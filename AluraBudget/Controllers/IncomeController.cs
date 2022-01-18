using AluraBudget.Data;
using AluraBudget.Data.DTO.IncomeDto;
using AluraBudget.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Linq;

namespace AluraBudget.Controllers
{
    [ApiController]
    [Route("/receitas")]
    public class IncomeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public IncomeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable Index()
        {
            return _context.Incomes;
        }

        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            Income income = FindById(id);
            if (income != null)
            {
                ReadIncomeDto incomeDto = _mapper.Map<ReadIncomeDto>(income);
                return Ok(incomeDto);
            }
            return NotFound();
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateIncomeDto incomeDto)
        {
            Income income = _mapper.Map<Income>(incomeDto);
            _context.Incomes.Add(income);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Create), income);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateIncomeDto incomeDto)
        {
            Income income = FindById(id);
            if (income == null)
            {
                return NotFound();
            }
            _mapper.Map(incomeDto, income);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Income income = FindById(id);
            if(income == null)
            {
                return NotFound();
            }
            _context.Remove(income);
            _context.SaveChanges();

            return NoContent();
        }

        private Income FindById(int id)
        {
            return _context.Incomes.FirstOrDefault(income => income.Id == id);
        }




    }
}
