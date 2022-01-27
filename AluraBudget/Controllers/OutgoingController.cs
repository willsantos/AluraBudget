using AluraBudget.Data;
using AluraBudget.Data.DTO.OutgoingDto;
using AluraBudget.Helpers;
using AluraBudget.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AluraBudget.Controllers
{
    [ApiController]
    [Route("/despesas")]
    public class OutgoingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OutgoingController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable Index([FromQuery] string descricao)
        {
            List<Outgoing> outgoings = _context.Outgoings.ToList(); 


            if (!string.IsNullOrEmpty(descricao))
            {
                return FindOutgoingByDescription(descricao);
                
            }

            return outgoings;
        }



        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {



            Outgoing outgoing = FindById(id);
            if (outgoing != null)
            {
                ReadOutgoingDto outgoingDto = _mapper.Map<ReadOutgoingDto>(outgoing);
                return Ok(outgoingDto);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateOutgoingDto outgoingDto)
        {
            Outgoing outgoing = _mapper.Map<Outgoing>(outgoingDto);


            if (FindOutgoingByDate(outgoing) > 0)
            {
                return BadRequest("Item já cadastrado");
            }

            _context.Outgoings.Add(outgoing);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Create), outgoingDto);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateOutgoingDto outgoingDto)
        {
            Outgoing outgoing = FindById(id);
            if (outgoing == null)
            {
                return NotFound();
            }

            _mapper.Map(outgoingDto, outgoing);

            if (FindOutgoingByDate(outgoing) > 0)
            {
                return BadRequest("Item já cadastrado");
            }

            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Outgoing outgoing = FindById(id);
            if (outgoing == null)
            {
                return NotFound();
            }
            _context.Remove(outgoing);
            _context.SaveChanges();

            return NoContent();
        }

        private IEnumerable FindOutgoingByDescription(string description)
        {
            return _context.Outgoings
                    .Where(o => o.Description == description)
                    .ToList();
        }
        private int FindOutgoingByDate(Outgoing outgoingDto)
        {
            return _context.Outgoings
                            .Where(o =>
                                o.Description == outgoingDto.Description &&
                                o.Date.Month == outgoingDto.Date.Month &&
                                o.Date.Year == outgoingDto.Date.Year
                            ).Count();
        }

        private Outgoing FindById(int id)
        {
            return _context.Outgoings.FirstOrDefault(outgoing => outgoing.Id == id);
        }

    }
}
