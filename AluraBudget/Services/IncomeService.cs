using AluraBudget.Data;
using AluraBudget.Data.DTO.IncomeDto;
using AluraBudget.Models;
using AutoMapper;
using FluentResults;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AluraBudget.Services
{
    public class IncomeService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public IncomeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadIncomeDto AddIncome(CreateIncomeDto incomeDto)
        {
            Income income = _mapper.Map<Income>(incomeDto);


            if (FindIncomeByDate(income) > 0)
            {
                return null;
            }

            _context.Incomes.Add(income);
            _context.SaveChanges();
            return _mapper.Map<ReadIncomeDto>(income);

        }

        public List<ReadIncomeDto> GetIncomes(string descricao)
        {
            List<Income> incomes = _context.Incomes.ToList();

            if (!string.IsNullOrEmpty(descricao))
            {

                List<ReadIncomeDto> readDto = _mapper.Map<List<ReadIncomeDto>>(FindIncomeByDescription(descricao));
                return readDto;
            }

            return _mapper.Map<List<ReadIncomeDto>>(incomes);
        }

        public List<ReadIncomeDto> GetIncomesByMonth(int month, int year)
        {
            ICollection incomes = FindIncomesByMonth(year, month);

            if (incomes.Count > 0)
            {
                List<ReadIncomeDto> incomeDto = _mapper.Map<List<ReadIncomeDto>>(incomes);
                return incomeDto;
            }
            return _mapper.Map<List<ReadIncomeDto>>(incomes);
        }

        public ReadIncomeDto GetIncomesById(int id)
        {
            Income income = _context.Incomes.FirstOrDefault(income => income.Id == id);
            if (income != null)
            {
                ReadIncomeDto incomeDto = _mapper.Map<ReadIncomeDto>(income);
                return incomeDto;
            }
            return null;
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

        public Result UpdateIncome(int id, UpdateIncomeDto incomeDto)
        {
            Income income = _context.Incomes.FirstOrDefault(income => income.Id == id);
            if (income == null)
            {
                return Result.Fail(new Error("Item não encontrado"));
            }
            _mapper.Map(incomeDto, income);

            if (FindIncomeByDate(income) > 0)
            {
                return Result.Fail("Item já cadastrado");
            }

            _context.SaveChanges();
            return Result.Ok();
        }

        public Result RemoveIncome(int id)
        {
            Income income = _context.Incomes.FirstOrDefault(income => income.Id == id);
            if (income == null)
            {
                return Result.Fail("Item não encontrado");
            }
            _context.Remove(income);
            _context.SaveChanges();
            return Result.Ok();
        }

        private List<Income> FindIncomeByDescription(string descricao)
        {
            return _context.Incomes
                .Where(i =>
                    i.Description == descricao
                )
                .ToList();
        }

        private Income FindById(int id)
        {
            return _context.Incomes.FirstOrDefault(income => income.Id == id);
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



    }
}
