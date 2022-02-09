using AluraBudget.Data.DTO.OutgoingDto;
using AluraBudget.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AluraBudget.Controllers
{
    [ApiController]
    [Route("/despesas")]
    public class OutgoingController : ControllerBase
    {
        private readonly OutgoingService _outgoingService;
        
        public OutgoingController(OutgoingService outgoingService)
        {
            _outgoingService = outgoingService;
        }

        [HttpGet]
        public IActionResult Index([FromQuery] string descricao)
        {
            List<ReadOutgoingDto> readDto = _outgoingService.GetOutgoings(descricao);
            if(readDto != null) return Ok(readDto);
            return NotFound();
        }

        [HttpGet("{year}/{month}")]
        public IActionResult ListOutgoingsByMonth(int year, int month)
        {
            List<ReadOutgoingDto> readDto = _outgoingService.GetOutgoingsByMonth(year, month);
            if(readDto != null) return Ok(readDto);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult Show(int id)
        {
            ReadOutgoingDto readDto = _outgoingService.GetOutgoingsById(id);
            if (readDto != null) return Ok(readDto);
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateOutgoingDto outgoingDto)
        {
            ReadOutgoingDto readDto = _outgoingService.AddOutgoing(outgoingDto);
            
            if(readDto == null) 
                return BadRequest("Item já cadastrado");
            return CreatedAtAction(nameof(Create), readDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateOutgoingDto outgoingDto)
        {
            Result result = _outgoingService.UpdateOutgoing(id, outgoingDto);
            if(result.IsFailed) return
                    NotFound(result.Errors.FirstOrDefault().Message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Result result = _outgoingService.RemoveOutgoing(id);

            if(result.IsFailed) return 
                    NotFound(result.Errors.FirstOrDefault().Message);
            return NoContent();
            
        }
    }
}
