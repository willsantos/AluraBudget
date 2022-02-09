using AluraBudget.Data;
using AluraBudget.Data.DTO.OutgoingDto;
using AluraBudget.Models;
using AutoMapper;
using FluentResults;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AluraBudget.Services
{
    public class OutgoingService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OutgoingService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ReadOutgoingDto> GetOutgoings(string descricao)
        {
            List<Outgoing> outgoings = _context.Outgoings.ToList();

            if (!string.IsNullOrEmpty(descricao))
            {
                List<ReadOutgoingDto> readDto = _mapper.Map<List<ReadOutgoingDto>>(FindOutgoingByDescription(descricao));
                return readDto;
            }

            return _mapper.Map<List<ReadOutgoingDto>>(outgoings);
        }

        private List<Outgoing> FindOutgoingByDescription(string description)
        {
            return _context.Outgoings
                    .Where(o => o.Description == description)
                    .ToList();
        }

        public List<ReadOutgoingDto> GetOutgoingsByMonth(int year, int month)
        {
            ICollection outgoings = FindOutgoingByMonth(year, month);

            if (outgoings.Count > 0)
            {
                List<ReadOutgoingDto> readDto = _mapper.Map<List<ReadOutgoingDto>>(outgoings);
                return readDto;
            }

            return _mapper.Map<List<ReadOutgoingDto>>(outgoings);
        }

        public ReadOutgoingDto AddOutgoing(CreateOutgoingDto outgoingDto)
        {
            Outgoing outgoing = _mapper.Map<Outgoing>(outgoingDto);

            if (FindOutgoingByDate(outgoing) > 0)
            {
                return null;
            }

            _context.Outgoings.Add(outgoing);
            _context.SaveChanges();

            return _mapper.Map<ReadOutgoingDto>(outgoing);
        }

        public Result UpdateOutgoing(int id, UpdateOutgoingDto outgoingDto)
        {
            Outgoing outgoing = FindById(id);

            if (outgoing == null)
            {
                return Result.Fail("Item não encontrado");
            }

            _mapper.Map(outgoingDto, outgoing);

            if (FindOutgoingByDate(outgoing) > 0)
            {
                return Result.Fail("Item já cadastrado");
            }

            _context.SaveChanges();
            return Result.Ok();
        }

        public Result RemoveOutgoing(int id)
        {
            Outgoing outgoing = FindById(id);
            if (outgoing == null)
            {
                return Result.Fail("Item não encontrado");
            }
            _context.Remove(outgoing);
            _context.SaveChanges();

            return Result.Ok();
        }

        public ReadOutgoingDto GetOutgoingsById(int id)
        {
            Outgoing outgoing = FindById(id);
            if (outgoing != null)
            {
                ReadOutgoingDto readDto = _mapper.Map<ReadOutgoingDto>(outgoing);
                return readDto;
            }
            return null;
        }
        private Outgoing FindById(int id)
        {
            return _context.Outgoings.FirstOrDefault(outgoing => outgoing.Id == id);
        }

        private ICollection FindOutgoingByMonth(int year, int month)
        {
            return _context.Outgoings
                .Where(o =>
                    o.Date.Year == year &&
                    o.Date.Month == month
                )
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

    }
}
