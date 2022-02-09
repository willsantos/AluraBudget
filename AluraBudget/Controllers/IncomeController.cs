using AluraBudget.Data;
using AluraBudget.Data.DTO.IncomeDto;
using AluraBudget.Models;
using AluraBudget.Services;
using AutoMapper;
using Castle.Core.Internal;
using FluentResults;
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
        private  IncomeService _incomeService;

        public IncomeController(IncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string descricao)
        {
            List<ReadIncomeDto> readDto =_incomeService.GetIncomes(descricao);
            if(readDto != null) return Ok(readDto);
            return NotFound();
            
        }

        [HttpGet("{year}/{month}")]
        public IActionResult ListIncomesByMonth(int month, int year)
        {

            List<ReadIncomeDto> readDto = _incomeService.GetIncomesByMonth(month, year);

            if (readDto != null) return Ok(readDto);
            return NoContent();

        }

        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {

            ReadIncomeDto readDto = _incomeService.GetIncomesById(id);
            if(readDto != null) return Ok(readDto);
            return NotFound();
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateIncomeDto incomeDto)
        {
            ReadIncomeDto readDto =  _incomeService.AddIncome(incomeDto);

            if (readDto == null)
                return BadRequest("Item já cadastrado");

            return CreatedAtAction(nameof(Create), readDto);
        }

       

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateIncomeDto incomeDto)
        {
            Result result =_incomeService.UpdateIncome(id, incomeDto);

            if (result.IsFailed) return NotFound(result.Errors.FirstOrDefault().Message);
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Result result = _incomeService.RemoveIncome(id);

            if(result.IsFailed) return NotFound(result.Errors.FirstOrDefault().Message);
            return NoContent();
        }

       

       

        


    }
}
