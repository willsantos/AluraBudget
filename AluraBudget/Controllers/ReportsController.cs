﻿using AluraBudget.Data;
using AluraBudget.Data.DTO.ReportsDto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AluraBudget.Controllers
{
    [ApiController]
    [Route("/resumo")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReportsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{year}/{month}")]
        public IActionResult Index(int year, int month)
        {
            decimal incomes = GetSumOfIncomes(year, month);
            decimal outgoings = GetSumOfOutgoings(year, month);
            List<ReportMonthSummaryByCategoryDto> categoriesValues =
                GetSumOfOutgoingsByCategory(year, month);

            var summary = new ReportMonthSummaryDto
            {
                TotalIncomes = Math.Round(incomes, 2),
                TotalOutgoings = Math.Round(outgoings, 2),
                Balance = Math.Round(incomes - outgoings, 2),
                TotalCategory = categoriesValues
            };


            return Ok(summary);
        }

        private List<ReportMonthSummaryByCategoryDto> GetSumOfOutgoingsByCategory(int year, int month)
        {
            return _context.Outgoings
                    .Where(ot =>
                        ot.Date.Year == year &&
                        ot.Date.Month == month
                    )
                    .GroupBy(c =>
                        c.Category
                    ).Select(c =>
                       new ReportMonthSummaryByCategoryDto
                       {
                           Category = c.Key,
                           TotalOutgoings = Math.Round(c.Sum(t => t.Value))

                       }

                    ).ToList();
        }

        private decimal GetSumOfOutgoings(int year, int month)
        {
            return _context.Outgoings
                .Where(o =>
                    o.Date.Year == year &&
                    o.Date.Month == month
                )
                .Sum(o =>
                    o.Value
                );
        }

        private decimal GetSumOfIncomes(int year, int month)
        {
            return _context.Incomes
                .Where(i =>
                    i.Date.Year == year &&
                    i.Date.Month == month
                )
                .Sum(i =>
                    i.Value
                );
        }
    }
}
