using AluraBudget.Data;
using AluraBudget.Data.DTO.IncomeDto;
using AluraBudget.Models;
using AutoMapper;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
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
        public IEnumerable Index([FromQuery] string descricao)
        {
            List<Income> incomes = _context.Incomes.ToList();

            if (!string.IsNullOrEmpty(descricao))
            {
                return FindIncomeByDescription(descricao);
            }

            return incomes;
        }

        [HttpGet("{year}/{month}")]
        public IActionResult ListIncomesByMonth(int month, int year)
        {
           ICollection incomes = FindIncomesByMonth(year, month);

            if (incomes.Count > 0)
            {
                return Ok(incomes);
            }

            return NoContent();

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
            

            if (FindIncomeByDate(income) > 0)
            {
                return BadRequest("Item já cadastrado esse mês");
            }


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

            if (FindIncomeByDate(income) > 0)
            {
                return BadRequest("Item já cadastrado");
            }

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Income income = FindById(id);
            if (income == null)
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

        private IEnumerable FindIncomeByDescription(string descricao)
        {
            return _context.Incomes
                .Where(i =>
                    i.Description == descricao
                )
                .ToList();
        }

        private ICollection FindIncomesByMonth(int year, int month)
        {

            return _context.Incomes
                .Where(i =>
                    i.Date.Year == year &&
                    i.Date.Month == month
                )
                .ToList();
        }

        private int FindIncomeByDate(Income incomeDto)
        {
            return _context.Incomes
                .Where(i =>
                    i.Description == incomeDto.Description &&
                    i.Date.Month == incomeDto.Date.Month &&
                    i.Date.Year == incomeDto.Date.Year
                ).Count();
        }



    }
}
